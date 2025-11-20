using System;
using AnimalArena.Animals.Components;
using AnimalArena.Animals.Core;
using AnimalArena.Animals.Core.Interactions;
using UnityEngine;

namespace AnimalArena.Animals.Systems
{
    public class AnimalCollisionSystem : IAnimalCollisionSystem
    {
        private readonly IInteractionResolver _interactionResolver;
        private readonly IAnimalsController _animalsController;

        public AnimalCollisionSystem(IInteractionResolver interactionResolver, IAnimalsController animalsController)
        {
            _interactionResolver = interactionResolver;
            _animalsController = animalsController;
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
                    _animalsController.Kill(animal2);
                    return true;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}