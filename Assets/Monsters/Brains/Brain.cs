﻿using UnityEngine;

namespace Monsters.Brains
{
    public abstract class Brain : ScriptableObject
    {
        public abstract void Initialise(ControllableBase controllable);
        public abstract void Act(ControllableBase controllable);
    }
}