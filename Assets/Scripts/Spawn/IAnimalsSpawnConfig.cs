using System.Collections.Generic;
using UnityEngine;

namespace AnimalArena.Spawn
{
    public interface IAnimalsSpawnConfig
    {
        Vector2 SpawnDelayRange { get; }
        IReadOnlyList<GameObject> AnimalPrefabsList { get; }
    }
}
