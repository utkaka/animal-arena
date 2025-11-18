using UnityEngine;

namespace AnimalArena.Animals.Collisions
{
    public interface IAnimalCollisionSystem
    {
        void RegisterAnimalsCollision(Animal animal, GameObject collisionObject);
    }
}