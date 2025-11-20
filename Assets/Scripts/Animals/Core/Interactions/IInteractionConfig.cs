using System.Collections.Generic;

namespace AnimalArena.Animals.Core.Interactions
{
    public interface IInteractionConfig
    {
        IReadOnlyCollection<InteractionRule> GetRules();
    }
}