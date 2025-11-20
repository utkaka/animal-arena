using System;
using UnityEngine;

namespace AnimalArena.Fx.Core
{
    public interface IEffect
    {
        event Action<IEffect> Complete;
        GameObject GameObject { get; }
        void Play();
    }
}