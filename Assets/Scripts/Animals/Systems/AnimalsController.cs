using System;
using System.Collections.Generic;
using AnimalArena.Animals.Core;
using AnimalArena.Assets.Core;
using AnimalArena.GameField.Core;
using UnityEngine;
using VContainer.Unity;

namespace AnimalArena.Animals.Systems
{
    public class AnimalsController : IAnimalsController, ITickable
    {
        public event Action<IAnimal> AnimalDied;
        public event Action<IAnimal> AnimalSpawned;
        public event Action<IAnimal> AnimalEaten;
        
        private IAssetProvider _assetProvider;
        private HashSet<IAnimal> _animals;
        private IFieldBounds _fieldBounds;

        public AnimalsController(IAssetProvider assetProvider, IFieldBounds fieldBounds)
        {
            _assetProvider = assetProvider;
            _fieldBounds = fieldBounds;
            _animals = new HashSet<IAnimal>();
        }

        public void OnSpawned(IAnimal animal)
        {
            _animals.Add(animal);
            AnimalSpawned?.Invoke(animal);
        }

        public void Kill(IAnimal animal)
        {
            if (animal.IsDead) return;
            AnimalDied?.Invoke(animal);
            AnimalEaten?.Invoke(animal);
            animal.MarkAsDead();
            _animals.Remove(animal);
            _assetProvider.Release(animal.GameObject);
        }

        public void Tick()
        {
            foreach (var animal in _animals)
            {
                Vector3 position = animal.GameObject.transform.position;
                if (!_fieldBounds.IsInside(position)) continue;
                animal.MoveTo(_fieldBounds.GetRandomPointInside());
            }
        }
    }
}