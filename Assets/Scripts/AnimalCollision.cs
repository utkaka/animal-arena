using UnityEngine;

[RequireComponent(typeof(Animal))]
[RequireComponent(typeof(Rigidbody))]
public class AnimalCollision : MonoBehaviour
{
    private Animal _animal;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _animal = GetComponent<Animal>();
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_animal.IsDead)
            return;

        if (!collision.collider.TryGetComponent<Animal>(out var other) || other.IsDead)
            return;

        Vector3 avgNormal = Vector3.zero;
        foreach (var c in collision.contacts)
            avgNormal += c.normal;
        avgNormal =
            avgNormal.sqrMagnitude > 0.001f
                ? avgNormal.normalized
                : (transform.position - other.transform.position).normalized;

        _rigidBody.AddForce(avgNormal * _animal.BounceMultiplier, ForceMode.Impulse);

        if (_animal.AnimalKind == Animal.Kind.Prey && other.AnimalKind == Animal.Kind.Prey)
        {
            return;
        }

        if (_animal.AnimalKind == Animal.Kind.Prey && other.AnimalKind == Animal.Kind.Predator)
        {
            _animal.Die();
            return;
        }

        if (_animal.AnimalKind == Animal.Kind.Predator && other.AnimalKind == Animal.Kind.Prey)
        {
            other.Die();
            return;
        }

        if (_animal.AnimalKind == Animal.Kind.Predator && other.AnimalKind == Animal.Kind.Predator)
        {
            bool iWin = Random.value > 0.5f;
            if (iWin)
                other.Die();
            else
                _animal.Die();
        }
    }
}
