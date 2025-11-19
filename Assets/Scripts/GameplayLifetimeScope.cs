using AnimalArena.Animals;
using AnimalArena.Animals.Collisions;
using AnimalArena.Animals.Interactions;
using AnimalArena.Animals.Movement;
using AnimalArena.Assets;
using AnimalArena.Fx;
using AnimalArena.GameField;
using AnimalArena.Spawn;
using AnimalArena.Statistics;
using AnimalArena.UI.Statistics;
using UnityEngine;
using UnityEngine.Serialization;
using VContainer;
using VContainer.Unity;

namespace AnimalArena
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [FormerlySerializedAs("_spawnConfig")] [SerializeField]
        private ScriptableAnimalsSpawnConfig animalsSpawnConfig;
        [SerializeField]
        private ScriptableInteractionsConfig _interactionsConfig;
        [SerializeField]
        private ScriptableEffectsConfig _effectsConfig;
        [SerializeField]
        private AnimalStatisticsView _statisticsView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(animalsSpawnConfig).As<IAnimalsSpawnConfig>();
            builder.RegisterEntryPoint<AnimalsSpawnSystem>();
            
            builder.Register<AssetProvider>(Lifetime.Singleton).As<IAssetProvider>();

            builder.Register<AnimalsController>(Lifetime.Singleton).As<IAnimalsController>();
            
            builder.Register<CameraField>(Lifetime.Singleton).As<IFieldBounds>();
            
            builder.RegisterInstance(_interactionsConfig).As<IInteractionConfig>();
            builder.Register<InteractionResolver>(Lifetime.Singleton).As<IInteractionResolver>();
            builder.Register<AnimalCollisionSystem>(Lifetime.Singleton).As<IAnimalCollisionSystem>();
            
            builder.RegisterInstance(_effectsConfig).As<IEffectsConfig>();
            builder.RegisterEntryPoint<CanvasEffectsSystem>();
            
            builder.RegisterEntryPoint<AnimalStatisticsModel>() .As<IAnimalStatisticsModel>();
            
            builder.RegisterEntryPoint<MovementSystem>().As<IMovementSystem>();

            builder.RegisterComponentInHierarchy<Camera>();

            builder.RegisterComponentInNewPrefab(_statisticsView, Lifetime.Singleton).As<IAnimalStatisticsView>();
            builder.RegisterEntryPoint<AnimalStatisticsViewModel>();
        }
    }
}
