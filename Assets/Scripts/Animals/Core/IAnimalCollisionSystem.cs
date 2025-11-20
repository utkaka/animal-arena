using AnimalArena.Animals.Components;
using UnityEngine;

namespace AnimalArena.Animals.Core
{
    public interface IAnimalCollisionSystem
    {
        void RegisterAnimalsCollision(Animal animal, GameObject collisionObject);
    }
}