using UnityEngine;

namespace AnimalArena.GameField.Core
{
    public interface IFieldBounds
    {
        bool IsInside(Vector3 position);
        Vector3 GetRandomPointInside();
    }
}
