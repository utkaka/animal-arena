using System.Collections.Generic;
using AnimalArena.Animals.Core;

namespace AnimalArena.Statistics.Core
{
    public interface IAnimalStatisticsModel
    {
        event System.Action<AnimalType, AnimalTypeStatistics> OnChanged;

        IReadOnlyDictionary<AnimalType, AnimalTypeStatistics> GetOverallStatistics();
        AnimalTypeStatistics GetTypeStatistics(AnimalType animalType);
    }
}