using System.Collections.Generic;
using AnimalArena.Animals;
using AnimalArena.Statistics;

namespace AnimalArena.UI.Statistics
{
    public interface IAnimalStatisticsView
    {
        void ShowStatistics(IReadOnlyDictionary<AnimalType, AnimalTypeStatistics> overallStatistics);
        void UpdateStatistics(AnimalType animalType, AnimalTypeStatistics statistics);
    }
}