using Common.Utilities;
using UnityEngine;

namespace Characters.Brains.Decisions
{
    [CreateAssetMenu(menuName = "Brains/Decisions/TimePassed")]
    public class TimePassed : BrainDecision
    {
        public RangedFloat waitTime;

        public override void Initialise(ControllableBase controllable)
        {
            controllable.MaxWaitTime = waitTime.GetRandomValue();
            controllable.WaitTime = 0;
        }

        public override bool Decide(ControllableBase controllable)
        {
            controllable.WaitTime += Time.deltaTime;
            return controllable.WaitTime > controllable.MaxWaitTime;
        }
    }
}