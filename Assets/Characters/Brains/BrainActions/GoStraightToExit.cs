using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/HeadToExit")]
    public class GoStraightToExit : BrainAction
    {
        public float speedMultiplier = 1;
        
        public override void Initialise(ControllableBase controllable)
        {
            controllable.TargetGameObject = GameObject.Find("VisitorEscapePointBuilding"); // TODO: String check bad
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.MoveTowards(controllable.TargetGameObject.transform.position, speedMultiplier);
        }
    }
}