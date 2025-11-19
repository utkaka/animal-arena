using AnimalArena.Animals;
using AnimalArena.Assets;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer;
using VContainer.Unity;

namespace AnimalArena.Spawn
{
    public class AnimalsSpawnSystem : ITickable
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IAnimalsSpawnConfig _animalsSpawnConfig;
        private readonly IAnimalsController _animalsController;
        private readonly Camera _camera;
        private float _nextSpawnTime;

        public AnimalsSpawnSystem(IAssetProvider assetProvider, IAnimalsSpawnConfig animalsSpawnConfig, IAnimalsController animalsController, Camera camera)
        {
            _assetProvider = assetProvider;
            _animalsSpawnConfig = animalsSpawnConfig;
            _animalsController = animalsController;
            _camera = camera;
            ScheduleNextSpawn();
        }

        public void Tick()
        {
            if (Time.time < _nextSpawnTime)
            {
                return;
            }
            SpawnObject();
            ScheduleNextSpawn();
        }

        private void ScheduleNextSpawn()
        {
            _nextSpawnTime =
                Time.time
                + Random.Range(_animalsSpawnConfig.SpawnDelayRange.x, _animalsSpawnConfig.SpawnDelayRange.y);
        }

        private void SpawnObject()
        {
            if (_animalsSpawnConfig.AnimalPrefabsList.Count == 0) return;
            Vector3 vp = new Vector3(Random.value, Random.value, 10f);
            Vector3 pos = _camera.ViewportToWorldPoint(vp);
            pos.y = 0f;

            var prefab = _animalsSpawnConfig.AnimalPrefabsList[Random.Range(0, _animalsSpawnConfig.AnimalPrefabsList.Count)];
            var instance = _assetProvider.Instantiate(prefab, pos, Quaternion.identity);
            var animal = instance.GetComponent<Animal>();
            Assert.IsNotNull(animal);
            _animalsController.OnSpawned(animal);
            var rb = instance.GetComponent<Rigidbody>();
            if (rb)
                rb.AddForce(
                    new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)),
                    ForceMode.VelocityChange
                );
        }
    }
}
