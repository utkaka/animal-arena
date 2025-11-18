using System.Collections.Generic;
using UnityEngine;

namespace AnimalArena.Spawn
{
    public interface ISpawnConfig
    {
        Vector2 SpawnDelayRange { get; }
        IReadOnlyList<GameObject> ObjectsList { get; }
    }
}
