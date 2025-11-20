using AnimalArena.Animals.Core;
using UnityEngine;
using VContainer;

namespace AnimalArena.Animals.Components
{
    public class AnimalCollider : MonoBehaviour
    {
        private IAnimal _animal;
        private IAnimalCollisionSystem _collisionSystem;

        [Inject]
        public void Construct(IAnimalCollisionSystem collisionSystem)
        {
            _collisionSystem = collisionSystem;
        }

        private void Awake()
        {
            _animal = GetComponent<IAnimal>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_animal.IsDead) return;
            //Prevent pair collision handling
            if (gameObject.GetInstanceID() > collision.gameObject.GetInstanceID()) return;
            _collisionSystem.RegisterAnimalsCollision(_animal, collision.gameObject);
        }
    }
}