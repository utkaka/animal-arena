using AnimalArena.Animals;
using AnimalArena.Statistics;
using TMPro;
using UnityEngine;

namespace AnimalArena.UI.Statistics
{
    public class AnimalStatisticsColumnView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _label;
        [SerializeField]
        private TextMeshProUGUI _deadLabel;
        [SerializeField]
        private TextMeshProUGUI _aliveLabel;
        
        public void Initialize(AnimalType type, AnimalTypeStatistics statistics)
        {
            _label.text = type.name;
            UpdateRow(statistics);
        }

        public void UpdateRow(AnimalTypeStatistics statistics)
        {
            //TODO: hardcoded strings & string interpolation are BAD, only for prototype
            _deadLabel.text = $"Dead: {statistics.Dead}";
            _aliveLabel.text = $"Alive: {statistics.Alive}";
        }
    }
}