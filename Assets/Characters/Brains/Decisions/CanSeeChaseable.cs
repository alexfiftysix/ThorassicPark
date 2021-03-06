using System.Linq;
using Characters.Visitors;
using Common.Utilities.Extensions;
using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu (menuName = "Brains/Decisions/CanSeeChaseable")]
    public class CanSeeChaseable : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override bool Decide(ControllableBase controllable)
        {
            // TODO: Change this to OverlapCircle. Use the returned Collider as the new target
            var colliders = Physics2D.OverlapCircleAll(controllable.transform.position, controllable.characterStats.viewRadius);
            if (colliders.Any(collider => collider.GetComponents<IChaseable>().Any()))
            {
                // TODO: This ain't efficient
                controllable.TargetChaseable = colliders
                    .RandomChoice(collider => collider.GetComponents<IChaseable>().Any()).gameObject
                    .GetComponent<IChaseable>();
                return true;
            }

            return false;
        }
    }
}