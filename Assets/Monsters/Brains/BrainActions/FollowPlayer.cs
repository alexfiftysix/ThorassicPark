using GameManagement;
using Phase_2.Player;
using UnityEngine;

namespace Monsters.Brains.BrainActions
{
    [CreateAssetMenu (menuName = "Brains/Actions/FollowPlayer")]
    public class FollowPlayer : BrainAction
    {
        public float speedMultiplier;
        
        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.Player == null) controllable.Player = FindObjectOfType<PlayerController>();
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.MoveTowards(controllable.Player.transform.position, speedMultiplier);
        }
    }
}