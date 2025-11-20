using AnimalArena.Animals.Components;
using UnityEngine;

namespace AnimalArena.Animals.Core
{
    public interface IAnimalCollisionSystem
    {
        void RegisterAnimalsCollision(IAnimal animal, GameObject collisionObject);
    }
}