using System.Linq;
using Phase_2.Player;
using UnityEngine;

namespace Monsters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/FoundPlayer")]
    public class FoundPlayer : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.Player == null) controllable.Player = FindObjectOfType<PlayerController>();
        }

        public override bool Decide(ControllableBase controllable)
        {
            var colliders = Physics2D.OverlapCircleAll(controllable.Player.transform.position, controllable.Player.viewRadiusSize);
            return colliders.Any(collider => collider.gameObject == controllable.gameObject);
        }
    }
}