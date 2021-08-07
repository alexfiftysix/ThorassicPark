using Extensions;
using GameManagement;
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

        private IChaseable _target;
        private ChaseableManager _chaseableManager;

        public override void Initialise(ControllableBase controllableBase)
        {
            _chaseableManager = FindObjectOfType<ChaseableManager>();
        }

        public override void Act(ControllableBase controllable)
        {
            if ((Object) _target == null)
            {
                FindTarget();
                return;
            }

            // rotation
            var currentAngle = controllable.EulerAngles.z;
            var targetAngle = controllable.Position.ToVector2().AngleTo(_target.Position);
            var rotationDegrees = Mathf.DeltaAngle(currentAngle, targetAngle);
            controllable.Rotate(rotationDegrees, rotationSpeed);

            // speed
            controllable.Move(Vector2.up, movementSpeed);
        }

        private void FindTarget()
        {
            _target = _chaseableManager.GetRandom();
        }
    }
}