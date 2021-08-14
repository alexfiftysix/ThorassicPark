using System.Collections.Generic;
using Characters.Brains.BrainStates;
using Characters.Brains.Decisions;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Characters.Brains.Transitions
{
    [System.Serializable]
    public class BrainTransition : BrainNode
    {
        public BrainDecision decision;
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

        public override Button GetAddButton(IMGUIContainer addContainer)
        {
            return new Button(() =>
            {
                var field = new ObjectField
                {
                    objectType = typeof(BrainDecision),
                    name = string.Empty,
                };
                addContainer.Add(field);
            })
            {
                text = "+"
            };
        }
    }
}