using UnityEngine;
using Visitors;

namespace Monsters.Brains
{
    public abstract class Brain : ScriptableObject
    {
        public abstract void Initialise();
        public abstract void Act(IControllable controllable, IChaseable target);
    }
}