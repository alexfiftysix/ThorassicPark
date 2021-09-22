using System.Collections.Generic;
using Characters.Brains.BrainStates;
using UnityEngine.UIElements;

namespace Characters.Brains
{
    public class RootNode : BrainNode
    {
        public BrainState startState;

        public void SetStartState(BrainState brainState)
        {
            startState = brainState;
        }

        public override List<BrainNode> GetChildren()
        {
            return new List<BrainNode> { startState };
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return other is BrainState;
        }

        public override void AddExtras(IMGUIContainer addContainer)
        {
        }
    }
}