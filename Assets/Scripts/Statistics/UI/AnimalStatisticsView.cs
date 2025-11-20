using System.Collections.Generic;
using AnimalArena.Animals.Core;
using AnimalArena.Statistics.Core;
using UnityEngine;
using UnityEngine.Assertions;

namespace AnimalArena.Statistics.UI
{
    public class AnimalStatisticsView : MonoBehaviour, IAnimalStatisticsView
    {
        [SerializeField]
        private GameObject _columnPrefab;
        [SerializeField]
        private RectTransform _columnContainer;
        
        private Dictionary<AnimalType, AnimalStatisticsColumnView> _columnByAnimal;

        public void ShowStatistics(IReadOnlyDictionary<AnimalType, AnimalTypeStatistics> overallStatistics)
        {
            _columnByAnimal =  new Dictionary<AnimalType, AnimalStatisticsColumnView>();
            foreach (var animalStatistics in overallStatistics)
            {
                GameObject instance = Instantiate(_columnPrefab, _columnContainer);
                AnimalStatisticsColumnView columnView = instance.GetComponent<AnimalStatisticsColumnView>();
                Assert.IsNotNull(columnView);
                _columnByAnimal.Add(animalStatistics.Key, columnView);
                columnView.Initialize(animalStatistics.Key, animalStatistics.Value);
            }
        }

        public void UpdateStatistics(AnimalType animalType, AnimalTypeStatistics statistics)
        {
            _columnByAnimal[animalType].UpdateRow(statistics);
        }
    }
}