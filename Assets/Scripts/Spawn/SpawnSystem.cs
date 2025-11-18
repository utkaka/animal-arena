using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace AnimalArena.Spawn
{
    public class SpawnSystem : ITickable
    {
        private readonly IObjectResolver _resolver;
        private readonly ISpawnConfig _spawnConfig;
        private readonly Camera _camera;
        private float _nextSpawnTime;

        public SpawnSystem(IObjectResolver resolver, ISpawnConfig spawnConfig, Camera camera)
        {
            _resolver = resolver;
            _spawnConfig = spawnConfig;
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
                + Random.Range(_spawnConfig.SpawnDelayRange.x, _spawnConfig.SpawnDelayRange.y);
        }

        private void SpawnObject()
        {
            if (_spawnConfig.ObjectsList.Count == 0) return;
            Vector3 vp = new Vector3(Random.value, Random.value, 10f);
            Vector3 pos = _camera.ViewportToWorldPoint(vp);
            pos.y = 0f;

            var prefab = _spawnConfig.ObjectsList[Random.Range(0, _spawnConfig.ObjectsList.Count)];
            var instance = _resolver.Instantiate(prefab, pos, Quaternion.identity);

            var rb = instance.GetComponent<Rigidbody>();
            if (rb)
                rb.AddForce(
                    new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)),
                    ForceMode.VelocityChange
                );
        }
    }
}
