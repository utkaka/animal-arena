using UnityEngine;

namespace AnimalArena.Assets.Core
{
    public interface IAssetProvider
    {
        GameObject Instantiate (GameObject prefab);
        GameObject Instantiate (GameObject prefab, Vector3 position);
        GameObject Instantiate (GameObject prefab, Vector3 position, Quaternion rotation);
        void Release(GameObject gameObject);
    }
}