using Common.Utilities.Extensions;
using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/TurnToTarget")]
    public class TurnToTarget : BrainAction
    {
        public float rotationSpeedMultiplier = 1;

        public override void Initialise(ControllableBase controllable)
        {
        }

        public override void Act(ControllableBase controllable)
        {
            if ((Object)controllable.TargetChaseable == null)
            {
                return;
            }

            var currentAngle = controllable.EulerAngles.z;
            var targetAngle = controllable.Position.ToVector2().AngleTo(controllable.TargetChaseable.Position);
            var rotationDegrees = Mathf.DeltaAngle(currentAngle, targetAngle);
            controllable.Rotate(rotationDegrees, rotationSpeedMultiplier);
        }
    }
}