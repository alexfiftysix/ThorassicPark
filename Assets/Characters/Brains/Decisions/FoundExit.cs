using System.Linq;
using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/FoundExit")]
    public class FoundExit : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
            controllable.TargetGameObject = GameObject.Find("VisitorEscapePointBuilding"); // TODO: String check bad
        }

        public override bool Decide(ControllableBase controllable)
        {
            var colliders = Physics2D.OverlapCircleAll(controllable.transform.position, controllable.ViewRadiusSize);
            return colliders.Any(collider => collider.gameObject == controllable.TargetGameObject);
        }
    }
}