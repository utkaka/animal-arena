using AnimalArena.Animals;
using AnimalArena.Statistics;
using VContainer.Unity;

namespace AnimalArena.UI.Statistics
{
    public class AnimalStatisticsViewModel : IInitializable
    {
        
        private readonly IAnimalStatisticsView  _view;
        private readonly IAnimalStatisticsModel _model;

        public AnimalStatisticsViewModel(IAnimalStatisticsView view, IAnimalStatisticsModel model)
        {
            _view = view;
            _model = model;
        }

        public void Initialize()
        {
            _view.ShowStatistics(_model.GetOverallStatistics());
            _model.OnChanged += ModelOnChanged;
        }

        private void ModelOnChanged(AnimalType type, AnimalTypeStatistics statistics)
        {
            _view.UpdateStatistics(type, statistics);
        }
    }
}