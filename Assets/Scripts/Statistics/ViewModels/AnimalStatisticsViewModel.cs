using System;
using AnimalArena.Animals.Core;
using AnimalArena.Statistics.Core;
using AnimalArena.Statistics.UI;
using VContainer.Unity;

namespace AnimalArena.Statistics.ViewModels
{
    public class AnimalStatisticsViewModel : IInitializable, IDisposable
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

        public void Dispose()
        {
            _model.OnChanged -= ModelOnChanged;
        }

        private void ModelOnChanged(AnimalType type, AnimalTypeStatistics statistics)
        {
            _view.UpdateStatistics(type, statistics);
        }
    }
}