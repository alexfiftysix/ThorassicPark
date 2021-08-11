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
    }
}