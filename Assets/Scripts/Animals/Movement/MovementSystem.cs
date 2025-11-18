using System.Collections.Generic;
using UnityEngine;
using VContainer.Unity;

namespace AnimalArena.Animals.Movement
{
    public class MovementSystem : IMovementSystem, IFixedTickable
    {
        private readonly HashSet<IMovementAgent> _agents = new();

        public void RegisterAgent(IMovementAgent agent)
        {
            _agents.Add(agent);
        }

        public void UnregisterAgent(IMovementAgent agent)
        {
            _agents.Remove(agent);
        }
        
        public void FixedTick()
        {
            float time = Time.fixedDeltaTime;
            foreach (var agent in _agents)
            {
                if (agent.State == MovementState.Stopped) continue;
                agent.DoUpdate(time);
            }
        }
    }
}