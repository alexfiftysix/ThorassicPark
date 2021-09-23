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
            var colliders = Physics2D.OverlapCircleAll(controllable.transform.position, controllable.viewRadius);
            if (colliders.Any(collider => collider.GetComponents<IChaseable>().Any()))
            {
                // TODO: This ain't efficient. Also I don't like that this decision introduces a side-effect
                controllable.TargetChaseable = colliders
                    .RandomChoice(collider => collider.GetComponents<IChaseable>().Any()).gameObject
                    .GetComponent<IChaseable>();
                return true;
            }

            return false;
        }
    }
}