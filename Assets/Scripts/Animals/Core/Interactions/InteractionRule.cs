namespace AnimalArena.Animals.Core.Interactions
{
    [System.Serializable]
    public class InteractionRule
    {
        public AnimalType Animal1;
        public AnimalType Animal2;
        public InteractionActionType ActionType;
        public bool Bidirectional;
    }
}