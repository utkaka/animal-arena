namespace AnimalArena.Animals.Movement
{
    public interface IMovementSystem
    {
        void RegisterAgent(IMovementAgent agent);
        void UnregisterAgent(IMovementAgent agent);
    }
}