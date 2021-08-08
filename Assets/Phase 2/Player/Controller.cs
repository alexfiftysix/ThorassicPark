using Monsters.Brains;
using Monsters.Brains.BrainActions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Phase_2.Player
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