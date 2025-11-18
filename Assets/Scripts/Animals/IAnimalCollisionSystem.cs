using UnityEngine;

namespace AnimalArena.Animals
{
    public interface IAnimalCollisionSystem
    {
        void RegisterAnimalsCollision(Animal animal, GameObject collisionObject);
    }
}