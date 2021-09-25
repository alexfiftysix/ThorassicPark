using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/TurnLeft")]
    public class TurnLeft : BrainAction
    {
        public float speed;

        public override void Initialise(ControllableBase controllable)
        {
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.Rotate(speed);
        }
    }
}