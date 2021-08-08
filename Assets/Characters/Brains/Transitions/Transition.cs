using Characters.Brains.BrainStates;
using Characters.Brains.Decisions;

namespace Characters.Brains.Transitions
{
    [System.Serializable]
    public class Transition
    {
        public BrainDecision decision;
        public BrainState nextState;
    }
}