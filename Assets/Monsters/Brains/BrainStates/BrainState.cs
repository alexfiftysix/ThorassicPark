using Monsters.Brains.BrainActions;
using UnityEngine;

namespace Monsters.Brains.BrainStates
{
    [CreateAssetMenu(menuName = "Brains/State")]
    public class BrainState : ScriptableObject
    {
        public BrainAction[] actions;
        public Transition[] transitions;

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
            if (actions.Length > 0)
            {
                foreach (var brainAction in actions)
                {
                    brainAction.Act(controllable);
                }
            }

            if (transitions.Length > 0)
            {
                foreach (var transition in transitions)
                {
                    controllable.TransitionToState(transition.decision.Decide(controllable)
                        ? transition.trueState
                        : transition.falseState
                    );
                }
            }
        }
    }
}