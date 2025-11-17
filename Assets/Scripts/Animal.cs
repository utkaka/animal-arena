using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class Animal : MonoBehaviour
{
    public enum Kind
    {
        Prey,
        Predator,
    }

    [SerializeField]
    private Kind _kind;

    [Header("Predator movement")]
    [SerializeField]
    private float _moveForce = 8f;

    [SerializeField]
    private float _maxSpeed = 6f;

    [SerializeField]
    private float _randomDirChangeInterval = 1.5f;

    [Header("Prey hopping")]
    [SerializeField]
    private float _hopMinInterval = 0.8f;

    [SerializeField]
    private float _hopMaxInterval = 1.6f;

    [SerializeField]
    private float _hopHorizontalImpulse = 4f;

    [SerializeField]
    private float _hopUpImpulse = 5f;

    [SerializeField]
    private LayerMask _groundMask = ~0;

    [Header("Physics")]
    [SerializeField]
    private float _bounceMultiplier = 1f;

    [SerializeField]
    private float _turnBackForce = 12f;

    private Rigidbody _rigidBody;
    private Vector3 _desiredDir;
    private float _nextDirChangeTime;

    private float _nextHopTime;
    private bool _isGrounded;

    public bool IsDead { get; private set; }
    public float BounceMultiplier => _bounceMultiplier;
    public Kind AnimalKind => _kind;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rigidBody.interpolation = RigidbodyInterpolation.Interpolate;
        _rigidBody.constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        IsDead = false;
        PickNewRandomDir();
        _nextDirChangeTime = Time.time + Random.Range(0.5f, _randomDirChangeInterval);

        if (_kind == Kind.Prey)
        {
            ScheduleNextHop();
        }
    }

    private void Update()
    {
        if (IsDead)
            return;

        if (_kind == Kind.Predator)
        {
            if (Time.time >= _nextDirChangeTime)
            {
                PickNewRandomDir();
                _nextDirChangeTime = Time.time + Random.Range(0.7f, _randomDirChangeInterval);
            }
        }
        else if (_kind == Kind.Prey)
        {
            if (Time.time >= _nextDirChangeTime)
            {
                PickNewRandomDir();
                _nextDirChangeTime = Time.time + Random.Range(1f, 2.2f);
            }

            UpdateGrounded();
            TryHop();
        }

        KeepOnScreen();
    }

    private void FixedUpdate()
    {
        if (IsDead)
            return;

        if (_kind == Kind.Predator)
        {
            _rigidBody.AddForce(_desiredDir * _moveForce, ForceMode.Acceleration);

            if (_rigidBody.velocity.magnitude > _maxSpeed)
                _rigidBody.velocity = _rigidBody.velocity.normalized * _maxSpeed;
        }
        else if (_kind == Kind.Prey)
        {
            Vector3 v = _rigidBody.velocity;
            Vector3 horizontal = new Vector3(v.x, 0f, v.z);
            if (horizontal.magnitude > _maxSpeed)
            {
                horizontal = horizontal.normalized * _maxSpeed;
                _rigidBody.velocity = new Vector3(horizontal.x, v.y, horizontal.z);
            }
        }
    }

    private void ScheduleNextHop()
    {
        _nextHopTime = Time.time + Random.Range(_hopMinInterval, _hopMaxInterval);
    }

    private void UpdateGrounded()
    {
        Vector3 origin = transform.position + Vector3.up * 0.1f;
        float rayDistance = 1.3f;

        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, rayDistance, _groundMask))
        {
            _isGrounded = true;
        }
        else
        {
            _isGrounded = false;
        }
    }

    private void TryHop()
    {
        if (!_isGrounded)
            return;
        if (Time.time < _nextHopTime)
            return;

        Vector3 dirXZ = new Vector3(_desiredDir.x, 0f, _desiredDir.z);
        if (dirXZ.sqrMagnitude < 0.01f)
        {
            dirXZ = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        }
        dirXZ = dirXZ.normalized;

        Vector3 v = _rigidBody.velocity;
        Vector3 horiz = new Vector3(v.x, 0f, v.z) * 0.5f;
        _rigidBody.velocity = new Vector3(horiz.x, Mathf.Min(v.y, 0f), horiz.z);

        Vector3 impulse = dirXZ * _hopHorizontalImpulse + Vector3.up * _hopUpImpulse;

        _rigidBody.AddForce(impulse, ForceMode.Impulse);

        ScheduleNextHop();
    }

    private void PickNewRandomDir()
    {
        var dir = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f));
        _desiredDir = dir.sqrMagnitude < 0.01f ? Vector3.forward : dir.normalized;
        transform.rotation = Quaternion.Slerp(
            transform.rotation,
            Quaternion.LookRotation(_desiredDir),
            0.5f
        );
    }

    private void KeepOnScreen()
    {
        var cam = Camera.main;
        if (!cam)
            return;

        Vector3 vp = cam.WorldToViewportPoint(transform.position);
        bool outX = vp.x < -0.02f || vp.x > 1.02f;
        bool outY = vp.y < -0.02f || vp.y > 1.02f;
        bool behind = vp.z < 0f;

        if (outX || outY || behind)
        {
            var center = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, Mathf.Max(5f, vp.z)));
            Vector3 toCenter = (
                new Vector3(center.x, transform.position.y, center.z) - transform.position
            ).normalized;
            _desiredDir = Vector3.Lerp(_desiredDir, toCenter, 0.5f).normalized;

            _rigidBody.AddForce(toCenter * _turnBackForce, ForceMode.Acceleration);
        }
    }

    public void Die()
    {
        if (IsDead)
            return;
        IsDead = true;
        gameObject.SetActive(false);
    }
}
