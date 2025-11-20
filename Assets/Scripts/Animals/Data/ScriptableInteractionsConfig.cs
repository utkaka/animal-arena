using System.Collections.Generic;
using AnimalArena.Animals.Core.Interactions;
using UnityEngine;

namespace AnimalArena.Animals.Data
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