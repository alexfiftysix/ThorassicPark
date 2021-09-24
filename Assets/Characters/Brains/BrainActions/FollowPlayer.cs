using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/FollowPlayer")]
    public class FollowPlayer : BrainAction
    {
        public float speedMultiplier;
        
        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.Player == null) controllable.Player = FindObjectOfType<Player.Player>();
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.MoveTowards(controllable.Player.transform.position, speedMultiplier);
        }
    }
}