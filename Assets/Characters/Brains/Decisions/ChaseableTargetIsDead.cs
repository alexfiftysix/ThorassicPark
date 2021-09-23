using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/ChaseableTargetIsDead")]
    public class ChaseableTargetIsDead : BrainDecision
    {
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override bool Decide(ControllableBase controllable)
        {
            return (Object)controllable.TargetChaseable == null;
        }
    }
}