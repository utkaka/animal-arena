using System.Collections.Generic;
using UnityEngine;

namespace AnimalArena.Animals.Interactions
{
    [CreateAssetMenu(fileName = "InteractionsConfig", menuName = "AnimalArena/Interactions Config", order = 0)]
    public class ScriptableInteractionsConfig : ScriptableObject, IInteractionConfig
    {
        [SerializeField]
        private InteractionRule[] _rules;

        public IReadOnlyCollection<InteractionRule> GetRules()
        {
            return _rules;
        }
    }
}