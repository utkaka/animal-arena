using System;
using AnimalArena.Animals.Core.Movement;
using UnityEngine;
using VContainer;

namespace AnimalArena.Animals.Components.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    internal class HopMovementAgent : MonoBehaviour, IMovementAgent
    {
        public event Action OnMovementCompleted;
        
        [SerializeField]
        private float _jumpHeight;
        [SerializeField]
        private float _maxJumpDistance;
        [SerializeField]
        private float _stopDistance;
        
        public MovementState State { get; private set; }
        
        private Rigidbody _rigidBody;
        private IMovementSystem _movementSystem;
        private Vector3 _targetPosition;
        private bool _isJumping;
        private bool _grounded;

        [Inject]
        public void Construct(IMovementSystem movementSystem)
        {
            _movementSystem =  movementSystem;
        }
        
        public void MoveTo(Vector3 target)
        {
            _targetPosition = target;
            State = MovementState.Moving;
            transform.rotation = Quaternion.LookRotation(target);
        }

        public void Stop()
        {
            State = MovementState.Stopped;
            _targetPosition = transform.position;
            _rigidBody.velocity = Vector3.zero;
        }

        public void DoUpdate(float deltaTime)
        {
            Vector3 toTarget = _targetPosition - _rigidBody.position;
            toTarget.y = 0f;
            float distance = toTarget.magnitude;

            if (distance <= _stopDistance)
            {
                Stop();
                OnMovementCompleted?.Invoke();
                return;
            }
            
            if (_grounded && !_isJumping)
            {
                JumpTowardsTarget(toTarget, distance);
            }
            
            if (_isJumping && _grounded && _rigidBody.velocity.y <= 0.01f)
            {
                _isJumping = false;
            }
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _movementSystem.RegisterAgent(this);
        }

        private void OnDisable()
        {
            _movementSystem.UnregisterAgent(this);
            State =  MovementState.Stopped;
        }

        private void JumpTowardsTarget(Vector3 target, float distance)
        {
            Vector3 dir = target.normalized;

            float gravity = Mathf.Abs(Physics.gravity.y);
            float verticalSpeed = Mathf.Sqrt(2f * gravity * Mathf.Max(_jumpHeight, 0.01f));
            float flightTime = 2f * verticalSpeed / gravity;

            float jumpDist = Mathf.Min(distance, _maxJumpDistance);
            Vector3 horizontalVelocity = dir * (jumpDist / flightTime);

            Vector3 jumpVelocity = horizontalVelocity + Vector3.up * verticalSpeed;

            _rigidBody.velocity = jumpVelocity;
            _isJumping = true;
            _grounded = false;
        }

        private void OnCollisionEnter(Collision collision)
        {
            CheckGroundedFromCollision(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            CheckGroundedFromCollision(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            _grounded = false;
        }

        private void CheckGroundedFromCollision(Collision collision)
        {
            foreach (var contact in collision.contacts)
            {
                if (!(Vector3.Dot(contact.normal, Vector3.up) > 0.5f)) continue;
                _grounded = true;
                return;
            }
        }
    }
}
