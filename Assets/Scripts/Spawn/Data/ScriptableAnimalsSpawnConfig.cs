using System.Collections.Generic;
using AnimalArena.Spawn.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace AnimalArena.Spawn.Data
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
