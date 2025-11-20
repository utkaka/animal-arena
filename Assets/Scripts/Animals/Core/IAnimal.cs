using UnityEngine;

namespace AnimalArena.Animals.Core
{
    public interface IAnimal
    {
        bool IsDead { get; }
        AnimalType Type { get; }
        GameObject GameObject { get; }
        void MoveTo(Vector3 destination);
        void MarkAsDead();
    }
}