using System;
using AnimalArena.Animals.Components;

namespace AnimalArena.Animals.Core
{
    public interface IAnimalsController
    {
        event Action<Animal> AnimalDied;
        event Action<Animal> AnimalSpawned;
        event Action<Animal> AnimalEaten;
        void OnSpawned(Animal animal);
        void Kill(Animal animal);
    }
}