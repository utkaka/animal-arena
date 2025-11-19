using UnityEngine;

namespace AnimalArena.Fx
{
    public interface IEffectsConfig
    {
        GameObject CanvasPrefab { get; }
        GameObject AteEffectPrefab { get; }
    }
}