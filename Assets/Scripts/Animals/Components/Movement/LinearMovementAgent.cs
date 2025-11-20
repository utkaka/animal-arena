using System;
using AnimalArena.Animals.Core.Movement;
using UnityEngine;
using VContainer;

namespace AnimalArena.Animals.Components.Movement
{
    [RequireComponent(typeof(Rigidbody))]
    public class LinearMovementAgent : MonoBehaviour, IMovementAgent
    {
        public event Action OnMovementCompleted;
        
        [SerializeField]
        private float _speed;
        [SerializeField]
        private float _stopDistance;
        
        public MovementState State { get; private set; }
        
        private Rigidbody _rigidBody;
        private IMovementSystem _movementSystem;
        private Vector3 _targetPosition;
        
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
            _targetPosition = _rigidBody.position;
            _rigidBody.velocity = Vector3.zero;
        }

        public void DoUpdate(float deltaTime)
        {
            Vector3 currentPosition = _rigidBody.position;
            Vector3 toTarget = _targetPosition - currentPosition;
            toTarget.y = 0f;
            float distance = toTarget.magnitude;
            if (distance <= _stopDistance)
            {
                Stop();
                OnMovementCompleted?.Invoke();
                return;
            }
                    
            Vector3 direction = toTarget / distance;
            float step = _speed * deltaTime;
            Vector3 newPos = currentPosition + direction * Mathf.Min(step, distance);

            _rigidBody.MovePosition(newPos);
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
    }
}
