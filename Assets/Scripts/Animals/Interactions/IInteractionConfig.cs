using System.Collections.Generic;

namespace AnimalArena.Animals.Interactions
{
    public interface IInteractionConfig
    {
        IReadOnlyCollection<InteractionRule> GetRules();
    }
}