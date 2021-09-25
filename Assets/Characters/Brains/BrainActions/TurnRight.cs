using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/TurnRight")]
    public class TurnRight : BrainAction
    {
        public float speed;

        public override void Initialise(ControllableBase controllable)
        {
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.Rotate(-speed);
        }
    }
}