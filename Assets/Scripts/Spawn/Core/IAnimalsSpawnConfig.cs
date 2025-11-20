using System.Collections.Generic;
using UnityEngine;

namespace AnimalArena.Spawn.Core
{
    public interface IAnimalsSpawnConfig
    {
        Vector2 SpawnDelayRange { get; }
        IReadOnlyList<GameObject> AnimalPrefabsList { get; }
    }
}
