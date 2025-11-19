using System.Collections.Generic;
using AnimalArena.Animals;

namespace AnimalArena.Statistics
{
    public interface IAnimalStatisticsModel
    {
        event System.Action<AnimalType, AnimalTypeStatistics> OnChanged;

        IReadOnlyDictionary<AnimalType, AnimalTypeStatistics> GetOverallStatistics();
        AnimalTypeStatistics GetTypeStatistics(AnimalType animalType);
    }
}