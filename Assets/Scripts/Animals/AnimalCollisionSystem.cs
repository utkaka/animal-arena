using System;
using AnimalArena.Animals.Interactions;
using UnityEngine;

namespace AnimalArena.Animals
{
    public class AnimalCollisionSystem : IAnimalCollisionSystem
    {
        private readonly IInteractionResolver _interactionResolver;

        public AnimalCollisionSystem(IInteractionResolver interactionResolver)
        {
            _interactionResolver = interactionResolver;
        }
        
        public void RegisterAnimalsCollision(Animal animal, GameObject collisionObject)
        {
            if (!collisionObject.TryGetComponent<Animal>(out var other)) return;
            if (other == null) return;
            if (TryPerformInteraction(animal, other)) return;
            TryPerformInteraction(other, animal);
        }

        private bool TryPerformInteraction(Animal animal1, Animal animal2)
        {
            InteractionActionType action = _interactionResolver.Resolve(animal1.Type, animal2.Type);
            switch (action)
            {
                case InteractionActionType.None:
                    break;
                case InteractionActionType.Eat:
                    animal2.Die();
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}