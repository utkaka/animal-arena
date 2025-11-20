using System;
using UnityEngine;

namespace AnimalArena.Animals.Core.Movement
{
    public interface IMovementAgent
    {
        event Action OnMovementCompleted;
        MovementState State { get; }
        void DoUpdate(float deltaTime);
        void MoveTo(Vector3 target);
        void Stop();
    }
}
