using UnityEngine;

namespace Characters.Brains.Decisions
{
    public abstract class BrainDecision : ScriptableObject
    {
        public abstract void Initialise(ControllableBase controllable);
        public abstract bool Decide(ControllableBase controllable);
    }
}