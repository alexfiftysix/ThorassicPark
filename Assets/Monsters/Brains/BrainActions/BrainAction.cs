using UnityEngine;

namespace Monsters.Brains.BrainActions
{
    public abstract class BrainAction : ScriptableObject
    {
        public abstract void Initialise(ControllableBase controllable);
        public abstract void Act(ControllableBase controllable);
    }
}