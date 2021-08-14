using System.Collections.Generic;
using UnityEngine.UIElements;

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

        public override Button GetAddButton(IMGUIContainer addContainer)
        {
            return null;
        }
    }
}