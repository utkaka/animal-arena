using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Pool;
using VContainer;
using VContainer.Unity;

namespace AnimalArena.Assets
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IObjectResolver _resolver;
        private Dictionary<GameObject, ObjectPool<GameObject>> _poolByPrefab;
        private Dictionary<GameObject, ObjectPool<GameObject>> _poolByInstance;
        
        public AssetProvider(IObjectResolver resolver)
        {
            _resolver = resolver;
            _poolByPrefab = new Dictionary<GameObject, ObjectPool<GameObject>>();
            _poolByInstance = new Dictionary<GameObject, ObjectPool<GameObject>>();
        }
        
        public GameObject Instantiate(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            if (!_poolByPrefab.ContainsKey(prefab))
            {
                _poolByPrefab.Add(prefab, new ObjectPool<GameObject>(createFunc: () => _resolver.Instantiate(prefab),
                    actionOnGet: OnGetInstance,
                    actionOnRelease: OnReleaseInstance,
                    actionOnDestroy: OnDestroyInstance,
                    collectionCheck: true,
                    defaultCapacity: 10,
                    maxSize: 20));
            }
            var pool = _poolByPrefab[prefab];
            var instance = pool.Get();
            _poolByInstance.Add(instance, pool);
            instance.transform.SetPositionAndRotation(position, rotation);
            return instance;
        }

        public void Release(GameObject gameObject)
        {
            Assert.IsTrue(_poolByInstance.ContainsKey(gameObject));
            var pool = _poolByInstance[gameObject];
            _poolByInstance.Remove(gameObject);
            pool.Release(gameObject);
        }
        
        private static void OnGetInstance(GameObject gameObject)
        {
            gameObject.SetActive(true);
        }
        
        private static void OnReleaseInstance(GameObject gameObject)
        {
            gameObject.SetActive(false);
        }
        
        private static void OnDestroyInstance(GameObject gameObject)
        {
            Object.Destroy(gameObject);
        }
    }
}