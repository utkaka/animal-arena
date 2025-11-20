using UnityEngine;

namespace AnimalArena.Fx.Core
{
    public interface IEffectsConfig
    {
        GameObject CanvasPrefab { get; }
        GameObject AteEffectPrefab { get; }
    }
}