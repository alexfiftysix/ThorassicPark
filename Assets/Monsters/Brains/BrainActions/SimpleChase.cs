﻿using GameManagement;
using UnityEngine;

namespace Monsters.Brains.BrainActions
{
    [CreateAssetMenu (menuName = "Brains/Actions/SimpleChase")]
    public class SimpleChase : BrainAction
    {
        public float movementSpeedMultiplier = 1;
        
        public override void Initialise(ControllableBase controllable)
        {
            if (controllable.ChaseableManager == null)
                controllable.ChaseableManager = FindObjectOfType<ChaseableManager>(); 
            controllable.TargetChaseable = controllable.ChaseableManager.GetRandom();
        }

        public override void Act(ControllableBase controllable)
        {
            if ((UnityEngine.Object) controllable.TargetChaseable == null)
            {
                controllable.TargetChaseable = controllable.ChaseableManager.GetRandom();
                return;
            }

            var direction = (controllable.TargetChaseable.Position - controllable.Position).normalized;
            controllable.Move(direction, movementSpeedMultiplier);
        }
    }
}