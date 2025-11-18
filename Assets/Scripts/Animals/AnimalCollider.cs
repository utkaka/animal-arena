using System;
using UnityEngine;
using VContainer;

namespace AnimalArena.Animals
{
    public class AnimalCollider : MonoBehaviour
    {
        private Animal _animal;
        private IAnimalCollisionSystem _collisionSystem;

        [Inject]
        public void Construct(IAnimalCollisionSystem collisionSystem)
        {
            _collisionSystem = collisionSystem;
        }

        private void Awake()
        {
            _animal = GetComponent<Animal>();
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