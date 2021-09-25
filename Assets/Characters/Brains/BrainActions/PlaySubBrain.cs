using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/PlaySubBrain")]
    public class PlaySubBrain : BrainAction
    {
        public Brain subBrain;

        public override void Initialise(ControllableBase controllable)
        {
            controllable.stateStack.Push(subBrain.rootNode.startState);
        }

        public override void Act(ControllableBase controllable)
        {
            // SubBrains are handled by the brainStack, no actions needed here
        }
    }
}