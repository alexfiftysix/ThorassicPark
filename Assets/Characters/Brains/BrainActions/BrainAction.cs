using UnityEngine;

namespace Characters.Brains.BrainActions
{
    public abstract class BrainAction : ScriptableObject
    {
        public abstract void Initialise(ControllableBase controllable);
        public abstract void Act(ControllableBase controllable);
    }
}