using UnityEngine;

namespace Characters.Brains.BrainActions
{
    public static class BrainActionUtilities
    {
        public static void MoveTowards(this ControllableBase controllable, Vector3 targetPosition, float speedMultiplier = 1)
        {
            var direction = (targetPosition - controllable.Position).normalized;
            controllable.Move(direction, speedMultiplier);
        }
    }
}