using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainActions;
using Characters.Brains.Transitions;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

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

            foreach (var decision in transitions.SelectMany(transition => transition.decisions))
            {
                decision.Initialise(controllable);
            }
        }

        /// <summary>
        /// Performs actions and checks transitions
        /// </summary>
        /// <param name="controllable"></param>
        /// <returns>next State if transition is successful or Null</returns>
        public BrainState DoActions(ControllableBase controllable)
        {
            foreach (var brainAction in actions)
            {
                brainAction.Act(controllable);
            }

            foreach (var transition in transitions)
            {
                if (transition.decisions.Any(decision => decision.Decide(controllable)))
                {
                    return transition.nextState;
                }
            }

            return null;
        }

        public void AddTransition(BrainTransition transition)
        {
            transitions.Add(transition);
        }

        public void RemoveTransition(BrainTransition transition)
        {
            transitions.Remove(transition);
        }

        public override List<BrainNode> GetChildren()
        {
            return transitions
                .Select(t => t as BrainNode)
                .ToList();
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return other is BrainTransition;
        }

        private void ActionChangedCallback(ChangeEvent<Object> evt)
        {
            if (evt.previousValue != null && evt.previousValue is BrainAction previousBrainAction)
            {
                actions.Remove(previousBrainAction);
            }

            if (evt.newValue != null && evt.newValue is BrainAction newBrainAction && !actions.Contains(newBrainAction))
            {
                actions.Add(newBrainAction);
            }
        }

        public override void AddExtras(IMGUIContainer addContainer)
        {
            foreach (var action in actions)
            {
                var field = new ObjectField
                {
                    objectType = typeof(BrainAction),
                    name = string.Empty,
                    value = action
                };

                field.RegisterValueChangedCallback(ActionChangedCallback);

                addContainer.Add(field);
            }

            var button = new Button(() =>
            {
                var field = new ObjectField
                {
                    objectType = typeof(BrainAction),
                    name = string.Empty,
                };
                field.RegisterValueChangedCallback(ActionChangedCallback);
                var addButton = addContainer.Children().Last();
                addContainer.Add(field);
                field.PlaceBehind(addButton);
            })
            {
                text = "+"
            };

            addContainer.Add(button);
        }
    }
}