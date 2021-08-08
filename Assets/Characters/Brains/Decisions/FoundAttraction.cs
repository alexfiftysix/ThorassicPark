using System.Linq;
using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu (menuName = "Brains/Decisions/FoundAttraction")]
    public class FoundAttraction : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override bool Decide(ControllableBase controllable)
        {
            var colliders = Physics2D.OverlapCircleAll(controllable.TargetAttraction.Position, controllable.TargetAttraction.viewRadiusSize * 0.9f);
            return colliders.Any(collider => collider.gameObject == controllable.gameObject);
        }
        
        
    }
}