using System.Linq;
using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/FoundPlayer")]
    public class FoundPlayer : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.Player == null) controllable.Player = FindObjectOfType<Player.Player>();
        }

        public override bool Decide(ControllableBase controllable)
        {
            // TODO: This should work fine, but seems all backwards. Use the visitors view radius and position?
            var colliders = Physics2D.OverlapCircleAll(controllable.Player.transform.position, controllable.Player.viewRadiusSize);
            return colliders.Any(collider => collider.gameObject == controllable.gameObject);
        }
    }
}