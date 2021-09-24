using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/SimpleChase")]
    public class SimpleChase : BrainAction
    {
        public float movementSpeedMultiplier = 1;
        
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override void Act(ControllableBase controllable)
        {
            if ((Object)controllable.TargetChaseable == null) return;

            var direction = (controllable.TargetChaseable.Position - controllable.Position).normalized;
            controllable.Move(direction, movementSpeedMultiplier);
        }
    }
}