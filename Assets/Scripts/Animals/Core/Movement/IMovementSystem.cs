namespace AnimalArena.Animals.Core.Movement
{
    public interface IMovementSystem
    {
        void RegisterAgent(IMovementAgent agent);
        void UnregisterAgent(IMovementAgent agent);
    }
}