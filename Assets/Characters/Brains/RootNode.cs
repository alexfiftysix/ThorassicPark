using Characters.Brains.BrainStates;

namespace Characters.Brains
{
    public class RootNode : BrainNode
    {
        public BrainState startState;

        public void SetStartState(BrainState brainState)
        {
            startState = brainState;
        }
    }
}