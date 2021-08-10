using Characters.Brains.BrainActions;
using Characters.Brains.Transitions;
using UnityEngine;

namespace Characters.Brains.BrainStates
{
    [CreateAssetMenu(menuName = "Brains/State")]
    public class BrainState : ScriptableObject
    {
        public BrainAction[] actions;
        public Transition[] transitions;

        // editor
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;

        public void Initialise(ControllableBase controllable)
        {
            Debug.Log($"Initialise state: {name}");
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
                    if (transition.decision.Decide(controllable))
                    {
                        controllable.TransitionToState(transition.nextState);
                        break;
                    }
                }
            }
        }
    }
}