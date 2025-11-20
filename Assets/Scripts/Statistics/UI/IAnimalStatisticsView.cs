using System.Collections.Generic;
using AnimalArena.Animals.Core;
using AnimalArena.Statistics.Core;

namespace AnimalArena.Statistics.UI
{
    public interface IAnimalStatisticsView
    {
        void ShowStatistics(IReadOnlyDictionary<AnimalType, AnimalTypeStatistics> overallStatistics);
        void UpdateStatistics(AnimalType animalType, AnimalTypeStatistics statistics);
    }
}