namespace AnimalArena.Animals.Interactions
{
    public interface IInteractionResolver
    {
        InteractionActionType Resolve(AnimalType animal1, AnimalType animal2);
    }
}