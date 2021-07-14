using GameManagement;
using Statistics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities.Extensions;

namespace Phase_1.Builder.DeckBuilder.Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        private Deck _deck;
        [SerializeField] private float achievementCheckInterval = 3;
        private Toaster _toaster;

        private void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void Start()
        {
            gameObject.AddTimer(achievementCheckInterval, CheckAchievements);
            _deck = gameObject.GetComponent<Deck>();
        }

        private void CheckAchievements()
        {
            _deck.unlockables.ForEach(u => u.Test(_toaster));
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            switch (scene.name)
            {
                case "Game":
                    _toaster = FindObjectOfType<Toaster>();
                    break;
                case "PostGame":
                    LockInUnlockables();
                    break;
            }
        }

        private void LockInUnlockables()
        {
            if (MyStatistics.wonLastGame)
            {
                _deck.unlockables.ForEach(u => u.LockItIn());
            }
        }
    }
}
