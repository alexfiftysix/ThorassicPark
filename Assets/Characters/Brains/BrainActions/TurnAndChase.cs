using Common.Utilities.Extensions;
using GameManagement;
using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/TurnAndChase")]
    public class TurnAndChase : BrainAction
    {
        public float movementSpeedMultiplier = 1;
        public float rotationSpeedMultiplier = 1;

        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.ChaseableManager == null)
                controllable.ChaseableManager = FindObjectOfType<ChaseableManager>();
            controllable.TargetChaseable = controllable.ChaseableManager.GetRandom();
        }

        public override void Act(ControllableBase controllable)
        {
            if ((Object)controllable.TargetChaseable == null)
            {
                controllable.TargetChaseable = controllable.ChaseableManager.GetRandom();
                return;
            }

            // rotation
            var currentAngle = controllable.EulerAngles.z;
            var targetAngle = controllable.Position.ToVector2().AngleTo(controllable.TargetChaseable.Position);
            var rotationDegrees = Mathf.DeltaAngle(currentAngle, targetAngle);
            controllable.Rotate(rotationDegrees, rotationSpeedMultiplier);

            // speed
            controllable.Move(Vector2.up, movementSpeedMultiplier);
        }
    }
}