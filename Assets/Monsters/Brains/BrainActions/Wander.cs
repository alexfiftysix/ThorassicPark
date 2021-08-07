using UnityEngine;
using Utilities;
using Utilities.Extensions;

namespace Monsters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/Wander")]
    public class Wander : BrainAction
    {
        // TODO
        // 1. Wandering
        // 2. Heading to attractions
        // 3. Running around scared
        // 4. Following Player
        // 5. Heading to Escape

        // TODO questions:
        // How to deal with move speed? Is it a property of the brain, the behaviour, the controllable (a combination?)
        //      A property of the controllable

        public RangedFloat turnAfter;

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

            controllable.Move(controllable.Direction, controllable.stats.movementSpeed);
        }
    }
}