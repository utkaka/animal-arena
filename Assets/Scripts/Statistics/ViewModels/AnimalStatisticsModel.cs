using System;
using System.Collections.Generic;
using AnimalArena.Animals.Components;
using AnimalArena.Animals.Core;
using AnimalArena.Spawn.Core;
using AnimalArena.Statistics.Core;
using UnityEngine.Assertions;
using VContainer.Unity;

namespace AnimalArena.Statistics.ViewModels
{
    public class AnimalStatisticsModel : IAnimalStatisticsModel, IInitializable, IDisposable
    {
        public event Action<AnimalType, AnimalTypeStatistics> OnChanged;
        
        private readonly IAnimalsController _animalsController;
        private readonly Dictionary<AnimalType, AnimalTypeStatistics> _statistics;
        private readonly IAnimalsSpawnConfig _animalsSpawnConfig;

        public AnimalStatisticsModel(IAnimalsController animalsController, IAnimalsSpawnConfig animalsSpawnConfig)
        {
            _animalsController = animalsController;
            _animalsSpawnConfig = animalsSpawnConfig;
            _statistics = new Dictionary<AnimalType, AnimalTypeStatistics>();
        }
        
        public void Initialize()
        {
            foreach (var animalPrefab in _animalsSpawnConfig.AnimalPrefabsList)
            {
                var animalComponent = animalPrefab.GetComponent<Animal>();
                Assert.IsNotNull(animalComponent);
                Assert.IsNotNull(animalComponent.Type);
                _statistics.TryAdd(animalComponent.Type, default);
            }
            _animalsController.AnimalSpawned += OnAnimalSpawned;
            _animalsController.AnimalDied += OnAnimalDied;
        }

        public void Dispose()
        {
            _animalsController.AnimalSpawned -= OnAnimalSpawned;
            _animalsController.AnimalDied -= OnAnimalDied;
        }

        private void OnAnimalSpawned(IAnimal animal) {
            AnimalType type = animal.Type;
            var animalStatistics = _statistics.GetValueOrDefault(type);
            animalStatistics.Alive++;
            _statistics[type] = animalStatistics;
            OnChanged?.Invoke(type, animalStatistics);
        }

        private void OnAnimalDied(IAnimal animal)
        {
            AnimalType type = animal.Type;
            var animalStatistics = _statistics.GetValueOrDefault(type);
            animalStatistics.Alive--;
            animalStatistics.Dead++;
            _statistics[type] = animalStatistics;
            OnChanged?.Invoke(type, animalStatistics);
        }

        public IReadOnlyDictionary<AnimalType, AnimalTypeStatistics> GetOverallStatistics() => _statistics;

        public AnimalTypeStatistics GetTypeStatistics(AnimalType animalType)
        {
            return _statistics.GetValueOrDefault(animalType);
        }
    }
}