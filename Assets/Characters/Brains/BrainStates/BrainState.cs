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

        public BrainAction action1;
        public BrainAction action2;
        
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
                RemoveAction(previousBrainAction);
            }

            if (evt.newValue != null && evt.newValue is BrainAction newBrainAction && !actions.Contains(newBrainAction))
            {
                AddAction(newBrainAction);
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