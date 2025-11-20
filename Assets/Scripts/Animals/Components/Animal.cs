using AnimalArena.Animals.Core;
using AnimalArena.Animals.Core.Movement;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AnimalArena.Animals.Components
{
    public class Animal : MonoBehaviour, IAnimal
    {
        [SerializeField]
        private AnimalType _type;
    
        public bool IsDead { get; private set; }
        public AnimalType Type => _type;
        public GameObject GameObject => gameObject;

        private IMovementAgent _movementAgent;

        public void MoveTo(Vector3 destination)
        {
            _movementAgent.MoveTo(destination);
        }

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
