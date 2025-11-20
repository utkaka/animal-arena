using System;
using System.Collections.Generic;
using AnimalArena.Animals.Components;
using AnimalArena.Animals.Core;
using AnimalArena.Assets;
using AnimalArena.Assets.Core;
using AnimalArena.GameField;
using AnimalArena.GameField.Core;
using UnityEngine;
using VContainer.Unity;

namespace AnimalArena.Animals.Systems
{
    public class AnimalsController : IAnimalsController, ITickable
    {
        public event Action<Animal> AnimalDied;
        public event Action<Animal> AnimalSpawned;
        public event Action<Animal> AnimalEaten;
        
        private IAssetProvider _assetProvider;
        private HashSet<Animal> _animals;
        private IFieldBounds _fieldBounds;

        public AnimalsController(IAssetProvider assetProvider, IFieldBounds fieldBounds)
        {
            _assetProvider = assetProvider;
            _fieldBounds = fieldBounds;
            _animals = new HashSet<Animal>();
        }

        public void OnSpawned(Animal animal)
        {
            _animals.Add(animal);
            AnimalSpawned?.Invoke(animal);
        }

        public void Kill(Animal animal)
        {
            if (animal.IsDead) return;
            AnimalDied?.Invoke(animal);
            AnimalEaten?.Invoke(animal);
            animal.MarkAsDead();
            _animals.Remove(animal);
            _assetProvider.Release(animal.gameObject);
        }

        public void Tick()
        {
            foreach (var animal in _animals)
            {
                Vector3 position = animal.transform.position;
                if (!_fieldBounds.IsInside(position)) continue;
                animal.MoveTo(_fieldBounds.GetRandomPointInside());
            }
        }
    }
}