using System.Linq;
using UnityEngine;
using Utilities.Extensions;

namespace Phase_1.Builder.DeckBuilder
{
    public class AchievementManager : MonoBehaviour
    {
        private Deck _deck;
        [SerializeField] private float achievementCheckInterval = 3;

        private void Start()
        {
            gameObject.AddTimer(achievementCheckInterval, CheckAchievements);
            _deck = gameObject.GetComponent<Deck>();
        }

        private void CheckAchievements()
        {
            foreach (var unlockable in _deck.unlockables.Where(u => !u.isUnlocked))
            {
                unlockable.Test();
            }
        }
    }
}
