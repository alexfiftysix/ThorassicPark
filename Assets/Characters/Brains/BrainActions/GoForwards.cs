using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu (menuName = "Brains/Actions/GoForwards")]
    public class GoForwards : BrainAction
    {
        public float movementSpeed;

        public override void Initialise(ControllableBase controllable)
        {
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.Move(Vector2.up * movementSpeed);
        }
    }
}
