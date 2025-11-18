using AnimalArena.Animals;
using AnimalArena.Animals.Collisions;
using AnimalArena.Animals.Interactions;
using AnimalArena.Animals.Movement;
using AnimalArena.Assets;
using AnimalArena.Spawn;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AnimalArena
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField]
        private ScriptableSpawnConfig _spawnConfig;
        [SerializeField]
        private ScriptableInteractionsConfig _interactionsConfig;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterInstance(_spawnConfig).As<ISpawnConfig>();
            builder.RegisterEntryPoint<SpawnSystem>();
            
            builder.Register<AssetProvider>(Lifetime.Singleton) .As<IAssetProvider>();
            
            builder.RegisterInstance(_interactionsConfig).As<IInteractionConfig>();
            builder.Register<InteractionResolver>(Lifetime.Singleton) .As<IInteractionResolver>();
            builder.Register<AnimalCollisionSystem>(Lifetime.Singleton) .As<IAnimalCollisionSystem>();
            
            builder.RegisterEntryPoint<MovementSystem>().As<IMovementSystem>();

            builder.RegisterComponentInHierarchy<Camera>();
        }
    }
}
