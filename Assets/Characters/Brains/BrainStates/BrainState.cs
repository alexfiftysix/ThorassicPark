using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainActions;
using Characters.Brains.Transitions;
using UnityEngine;

namespace Characters.Brains.BrainStates
{
    [CreateAssetMenu(menuName = "Brains/State")]
    public class BrainState : BrainNode
    {
        public List<BrainAction> actions = new List<BrainAction>();
        public List<BrainTransition> transitions = new List<BrainTransition>();

        public void Initialise(ControllableBase controllable)
        {
            foreach (var brainAction in actions)
            {
                brainAction.Initialise(controllable);
            }

            foreach (var transition in transitions)
            {
                transition.decision.Initialise(controllable);
            }
        }

        public void DoActions(ControllableBase controllable)
        {
            if (actions.Count > 0)
            {
                foreach (var brainAction in actions)
                {
                    brainAction.Act(controllable);
                }
            }

            if (transitions.Count > 0)
            {
                foreach (var transition in transitions)
                {
                    if (transition.decision.Decide(controllable))
                    {
                        controllable.TransitionToState(transition.nextState);
                        break;
                    }
                }
            }
        }

        public void AddTransition(BrainTransition transition)
        {
            transitions.Add(transition);
        }

        public void RemoveTransition(BrainTransition transition)
        {
            transitions.Remove(transition);
        }

        public void AddAction(BrainAction action)
        {
            actions.Add(action);
        }

        public void RemoveAction(BrainAction action)
        {
            actions.Remove(action);
        }

        public override List<BrainNode> GetChildren()
        {
            return transitions
                .Select(t => t as BrainNode)
                .Union(actions.Select(a => a as BrainNode))
                .ToList();
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return other is BrainTransition || other is BrainAction;
        }
    }
}