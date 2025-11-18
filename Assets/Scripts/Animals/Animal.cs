using AnimalArena.Animals.Movement;
using AnimalArena.Assets;
using UnityEngine;
using VContainer;
using Random = UnityEngine.Random;

namespace AnimalArena.Animals
{
    public class Animal : MonoBehaviour
    {
        [SerializeField]
        private AnimalType _type;
    
        public bool IsDead { get; private set; }
        public AnimalType Type => _type;

        private IMovementAgent _movementAgent;

        private void Awake()
        {
            _movementAgent = GetComponent<IMovementAgent>();
        }

        private void OnEnable()
        {
            IsDead = false;
            _movementAgent.OnMovementCompleted += OnMovementCompleted;
            PickNewRandomDir();
        }

        private void OnDisable()
        {
            _movementAgent.OnMovementCompleted -= OnMovementCompleted;
        }

        private void OnMovementCompleted()
        {
            PickNewRandomDir();
        }

        private void PickNewRandomDir()
        {
            var dir = new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            _movementAgent.MoveTo(dir);
        }

        public void MarkAsDead()
        {
            IsDead = true;
            _movementAgent.Stop();
        }
    }
}
