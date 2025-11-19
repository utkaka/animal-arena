using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimalArena.Spawn
{
    [CreateAssetMenu(fileName = "SpawnConfig.asset", menuName = "AnimalArena/Spawn Config")]
    public class ScriptableAnimalsSpawnConfig : ScriptableObject, IAnimalsSpawnConfig
    {
        [SerializeField]
        private Vector2 _spawnDelayRange;

        [FormerlySerializedAs("_objects")] [SerializeField]
        private GameObject[] _animalPrefabs;

        public Vector2 SpawnDelayRange => _spawnDelayRange;

        public IReadOnlyList<GameObject> AnimalPrefabsList => _animalPrefabs;
    }
}
