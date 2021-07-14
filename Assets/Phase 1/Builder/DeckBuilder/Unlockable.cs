using System;
using Phase_1.Builder.Buildings;
using Phase_1.Builder.DeckBuilder.Achievements;
using Statistics;
using UnityEngine;

namespace Phase_1.Builder.DeckBuilder
{
    [CreateAssetMenu(menuName = "Achievement")]
    public class Unlockable : ScriptableObject
    {
        public Attraction attraction;
        public bool isUnlocked;
        // Once conditions are met, the unlockable becomes 'ready.' It's not 'unlocked' until the player escapes.
        private bool _readyToUnlock;

        public float perGameMoneyRequired;
        public float perGameVisitorsRequired;

        private void OnEnable()
        {
            _readyToUnlock = isUnlocked;
        }

        public void Test(Toaster toaster)
        {
            if (isUnlocked || _readyToUnlock) return;

            if (MyStatistics.moneyEarned >= perGameMoneyRequired &&
                MyStatistics.visitorsSaved >= perGameVisitorsRequired)
            {
                _readyToUnlock = true;
                toaster.Add($"{attraction.name}");
            }
        }

        public void LockItIn()
        {
            if (isUnlocked || !_readyToUnlock) return;

            isUnlocked = true;
        }
    }
}