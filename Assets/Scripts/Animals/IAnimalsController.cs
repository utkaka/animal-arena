namespace AnimalArena.Animals
{
    public interface IAnimalsController
    {
        void OnSpawned(Animal animal);
        void Kill(Animal animal);
    }
}