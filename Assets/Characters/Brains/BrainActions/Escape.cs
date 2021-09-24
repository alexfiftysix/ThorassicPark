using UnityEngine;

namespace Characters.Brains.BrainActions
{
    [CreateAssetMenu(menuName = "Brains/Actions/VisitorEscape")]
    public class Escape : BrainAction
    {
        public override void Initialise(ControllableBase controllable)
        {
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.GameManager.VisitorEscaped();
            Destroy(controllable.gameObject);
        }
    }
}