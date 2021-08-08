using Buildings;
using Common.Utilities.Extensions;
using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu (menuName = "Brains/Actions/HeadToAttraction")]
    public class HeadToAttraction : BrainAction
    {
        public override void Initialise(ControllableBase controllable)
        {
            controllable.TargetAttraction = FindObjectsOfType<Attraction>().RandomChoice();
        }

        public override void Act(ControllableBase controllable)
        {
            if (controllable.TargetAttraction == null)
            {
                controllable.TargetAttraction = FindObjectsOfType<Attraction>().RandomChoice();
            }
            
            controllable.MoveTowards(controllable.TargetAttraction.Position);
        }
    }
}