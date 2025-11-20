using AnimalArena.GameField.Core;
using UnityEngine;

namespace AnimalArena.GameField.Data
{
    public class CameraField : IFieldBounds
    {
        private Rect _bounds;
        
        public CameraField(Camera camera)
        {
            _bounds = CalculateCameraBounds(camera);
        }
        
        public bool IsInside(Vector3 position)
        {
            return _bounds.Contains(new Vector2(position.x, position.z));
        }

        public Vector3 GetRandomPointInside()
        {
            return new Vector3(Random.value * _bounds.x, 0f, Random.value * _bounds.y);
        }

        private Rect CalculateCameraBounds(Camera camera)
        {
            float cameraY = camera.transform.position.y;
            Vector3 bottomLeft = camera.ViewportToWorldPoint(new Vector3(0f, 0f, cameraY));
            Vector3 topRight   = camera.ViewportToWorldPoint(new Vector3(1f, 1f, cameraY));
            return new Rect(bottomLeft.x, bottomLeft.z, topRight.x - bottomLeft.x, topRight.z - bottomLeft.z);
        }
    }
}