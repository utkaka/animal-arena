using System;
using System.Collections.Generic;
using AnimalArena.Assets;
using AnimalArena.GameField;
using UnityEngine;
using VContainer.Unity;

namespace AnimalArena.Animals
{
    public class AnimalsController : IAnimalsController, ITickable
    {
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
        }

        public void Kill(Animal animal)
        {
            if (animal.IsDead) return;
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