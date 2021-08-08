using GameManagement;
using UnityEngine;

namespace Monsters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/ParkHasBroken")]
    public class ParkHasBroken : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.GameManager == null)
            {
                controllable.GameManager = FindObjectOfType<GameManager>();
            }
        }

        public override bool Decide(ControllableBase controllable)
        {
            return controllable.GameManager.parkIsBroken;
        }
    }
}