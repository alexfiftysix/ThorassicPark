using System.Collections.Generic;
using Characters.Brains.BrainStates;
using Characters.Brains.Decisions;

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
    }
}