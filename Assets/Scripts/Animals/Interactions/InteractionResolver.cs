using System.Collections.Generic;

namespace AnimalArena.Animals.Interactions
{
    public class InteractionResolver : IInteractionResolver
    {
        private readonly Dictionary<(AnimalType, AnimalType), InteractionActionType> _map;
        
        public InteractionResolver(IInteractionConfig interactionConfig)
        {
            _map = BuildInteractionMap(interactionConfig);
        }
        
        public InteractionActionType Resolve(AnimalType animal1, AnimalType animal2)
        {
            if (!animal1 || !animal2) return InteractionActionType.None;
            return _map.GetValueOrDefault((animal1, animal2), InteractionActionType.None);
        }

        private Dictionary<(AnimalType, AnimalType), InteractionActionType> BuildInteractionMap(IInteractionConfig interactionConfig)
        {
            Dictionary<(AnimalType, AnimalType), InteractionActionType> result = new Dictionary<(AnimalType, AnimalType), InteractionActionType>();
            foreach (var rule in interactionConfig.GetRules())
            {
                if (!rule.Animal1 || !rule.Animal2) continue;

                var key = (rule.Animal1, rule.Animal2);
                result[key] = rule.ActionType;

                if (!rule.Bidirectional) continue;
                var reverseKey = (rule.Animal2, rule.Animal1);
                result[reverseKey] = rule.ActionType;
            }

            return result;
        }
    }
}