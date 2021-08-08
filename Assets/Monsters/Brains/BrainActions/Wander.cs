using UnityEngine;
using Utilities;
using Utilities.Extensions;

namespace Monsters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/Wander")]
    public class Wander : BrainAction
    {
        public RangedFloat turnAfter;
        public float moveSpeedMultiplier = 1;

        public override void Initialise(ControllableBase controllable)
        {
            controllable.MaxDecisionTime = turnAfter.GetRandomValue();
            controllable.Direction = Directions.directions.RandomChoice();
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.TimeSinceLastDecision += Time.deltaTime;
            if (controllable.TimeSinceLastDecision > controllable.MaxDecisionTime)
            {
                controllable.Direction = Directions.directions.RandomChoice();
                controllable.TimeSinceLastDecision -= controllable.MaxDecisionTime;
            }

            controllable.Move(controllable.Direction, moveSpeedMultiplier);
        }
    }
}