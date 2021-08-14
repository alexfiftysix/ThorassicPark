using System.Collections.Generic;

namespace Characters.Brains.BrainActions
{
    public abstract class BrainAction : BrainNode
    {
        public abstract void Initialise(ControllableBase controllable);
        public abstract void Act(ControllableBase controllable);

        public override List<BrainNode> GetChildren()
        {
            return new List<BrainNode>();
        }

        public override bool CanConnectTo(BrainNode other)
        {
            return false;
        }
    }
}