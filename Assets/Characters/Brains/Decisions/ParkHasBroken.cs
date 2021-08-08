using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/ParkHasBroken")]
    public class ParkHasBroken : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override bool Decide(ControllableBase controllable)
        {
            return controllable.GameManager.parkIsBroken;
        }
    }
}