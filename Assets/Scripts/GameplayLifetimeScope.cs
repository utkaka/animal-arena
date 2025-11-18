using AnimalArena.Animals.Movement;
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

        protected override void Configure(IContainerBuilder builder)
        {
        
            builder.RegisterInstance(_spawnConfig).As<ISpawnConfig>();
            builder.RegisterEntryPoint<SpawnSystem>();
            builder.RegisterEntryPoint<MovementSystem>().As<IMovementSystem>();

            builder.RegisterComponentInHierarchy<Camera>();
        }
    }
}
