using Extensions;
using UnityEngine;
using Utilities.Extensions;
using Visitors;

namespace Monsters.Brains
{
    [CreateAssetMenu(menuName = "Brains/TurningChaser")]
    public class TurningChaser : Brain
    {
        public float movementSpeed;
        public float rotationSpeed;

        public override void Act(IControllable controllable, IChaseable target)
        {
            // rotation
            var currentAngle = controllable.EulerAngles.z;
            var targetAngle = controllable.Position.ToVector2().AngleTo(target.Position);
            var rotationDegrees = Mathf.DeltaAngle(currentAngle, targetAngle);
            controllable.Rotate(rotationDegrees, rotationSpeed);
            
            // speed
            controllable.Move(Vector2.up, movementSpeed);
        }
    }
}