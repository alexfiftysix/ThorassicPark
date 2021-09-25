using System.Collections.Generic;
using System.Linq;
using Characters.Brains.BrainStates;
using Common.Utilities.Extensions;
using UnityEngine.UIElements;

namespace Characters.Brains.UtilityNodes
{
    public class RandomNode : BrainNode
    {
        public List<BrainState> children = new List<BrainState>();

        public void AddState(BrainState state)
        {
            children.Add(state);
        }

        public void RemoveState(BrainState brainState)
        {
            children.Remove(brainState);
        }
        
        public override List<BrainNode> GetChildren()
        {
            return children.Select(c => c as BrainNode).ToList();
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return other is BrainState;
        }

        public override void AddExtras(IMGUIContainer addContainer)
        {
        }

        public BrainState GetNextState()
        {
            return children.RandomChoice() as BrainState;
        }
    }
}