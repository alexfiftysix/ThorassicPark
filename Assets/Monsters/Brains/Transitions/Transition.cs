using Monsters.Brains.BrainStates;
using Monsters.Brains.Decisions;

namespace Monsters.Brains
{
    [System.Serializable]
    public class Transition
    {
        public BrainDecision decision;
        public BrainState nextState;
    }
}