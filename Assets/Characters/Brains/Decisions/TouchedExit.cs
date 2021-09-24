using System.Linq;
using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/TouchedExit")]
    public class TouchedExit : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override bool Decide(ControllableBase controllable)
        {
            var exit = GameObject.Find("VisitorEscapePointBuilding"); // TODO: That's gonna be slow

            var colliders = Physics2D.OverlapCircleAll(controllable.transform.position, controllable.characterStats.touchRadius);
            return colliders.Any(collider => collider.gameObject == exit.gameObject);
        }
    }
}