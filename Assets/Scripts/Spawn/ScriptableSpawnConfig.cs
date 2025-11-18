using System.Collections.Generic;
using UnityEngine;

namespace AnimalArena.Spawn
{
    [CreateAssetMenu(fileName = "SpawnConfig.asset", menuName = "AnimalArena/Spawn Config")]
    public class ScriptableSpawnConfig : ScriptableObject, ISpawnConfig
    {
        [SerializeField]
        private Vector2 _spawnDelayRange;

        [SerializeField]
        private GameObject[] _objects;

        public Vector2 SpawnDelayRange => _spawnDelayRange;

        public IReadOnlyList<GameObject> ObjectsList => _objects;
    }
}
