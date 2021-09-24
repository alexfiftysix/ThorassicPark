using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainStates;
using Characters.Brains.Decisions;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Characters.Brains.Transitions
{
    [System.Serializable]
    public class BrainTransition : BrainNode
    {
        public List<BrainDecision> decisions = new List<BrainDecision>();
        public BrainState nextState;

        public void SetNextState(BrainState brainState)
        {
            nextState = brainState;
        }

        public override List<BrainNode> GetChildren()
        {
            return new List<BrainNode> { nextState };
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return other is BrainState;
        }

        private void DecisionChangedCallback(ChangeEvent<Object> evt)
        {
            if (evt.previousValue != null && evt.previousValue is BrainDecision previousDecision &&
                decisions.Contains(previousDecision))
            {
                decisions.Remove(previousDecision);
            }

            if (evt.newValue != null && evt.newValue is BrainDecision newDecision && !decisions.Contains(newDecision))
            {
                decisions.Add(newDecision);
            }
        }

        public override void AddExtras(IMGUIContainer addContainer)
        {
            foreach (var decision in decisions)
            {
                var field = new ObjectField
                {
                    objectType = typeof(BrainDecision),
                    name = string.Empty,
                    value = decision
                };

                field.RegisterValueChangedCallback(DecisionChangedCallback);

                addContainer.Add(field);
            }

            addContainer.Add(
                new Button(() =>
                {
                    var field = new ObjectField
                    {
                        objectType = typeof(BrainDecision),
                        name = string.Empty,
                    };
                    field.RegisterValueChangedCallback(DecisionChangedCallback);
                    var addButton = addContainer.Children().Last();
                    addContainer.Add(field);
                    field.PlaceBehind(addButton);
                })
                {
                    text = "+"
                });
        }
    }
}