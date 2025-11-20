using System;

namespace AnimalArena.Animals.Core
{
    public interface IAnimalsController
    {
        event Action<IAnimal> AnimalDied;
        event Action<IAnimal> AnimalSpawned;
        event Action<IAnimal> AnimalEaten;
        void OnSpawned(IAnimal animal);
        void Kill(IAnimal animal);
    }
}