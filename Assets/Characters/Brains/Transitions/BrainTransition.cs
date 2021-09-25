using System;
using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainStates;
using Characters.Brains.Decisions;
using Characters.Brains.UtilityNodes;
using UnityEditor.UIElements;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Characters.Brains.Transitions
{
    [Serializable]
    public class BrainTransition : BrainNode
    {
        public List<BrainDecision> decisions = new List<BrainDecision>();
        [FormerlySerializedAs("nextState")] public BrainNode nextNode;


        public void SetNextNode(BrainNode brainNode)
        {
            if (!CanConnectTo(brainNode)) throw new Exception("Can't add child");
            nextNode = brainNode;
        }

        public BrainState GetNextState()
        {
            if (nextNode is RandomNode randomNode)
            {
                return randomNode.GetNextState();
            }

            return nextNode as BrainState;
        }

        public override List<BrainNode> GetChildren()
        {
            return new List<BrainNode> { nextNode };
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return other is BrainState || other is RandomNode;
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