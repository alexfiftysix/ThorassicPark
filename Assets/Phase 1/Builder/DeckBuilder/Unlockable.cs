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

        public float perGameMoneyRequired;
        public float perGameVisitorsRequired;

        public void Test(Toaster toaster)
        {
            if (isUnlocked) return;

            if (MyStatistics.moneyEarned >= perGameMoneyRequired &&
                MyStatistics.visitorsSaved >= perGameVisitorsRequired)
            {
                isUnlocked = true;
                toaster.Add($"Unlocked: {attraction.name}");
            }
        }
    }
}