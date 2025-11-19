using System;
using UnityEngine;

namespace AnimalArena.Fx
{
    public interface IEffect
    {
        event Action<IEffect> Complete;
        GameObject GameObject { get; }
        void Play();
    }
}