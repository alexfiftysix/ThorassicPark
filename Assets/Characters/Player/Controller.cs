using Characters.Brains;
using Characters.Brains.BrainActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Characters.Player
{
    [CreateAssetMenu (menuName = "Brains/Actions/HumanController")]
    public class Controller : BrainAction
    {
        public InputActionAsset actions;

        public override void Initialise(ControllableBase controllable)
        {
            actions["Move"].Enable();
        }

        public override void Act(ControllableBase controllable)
        {
            controllable.Move(actions["Move"].ReadValue<Vector2>());
        }
    }
}