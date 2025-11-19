using UnityEngine;

namespace AnimalArena.GameField
{
    public interface IFieldBounds
    {
        bool IsInside(Vector3 position);
        Vector3 GetRandomPointInside();
    }
}
