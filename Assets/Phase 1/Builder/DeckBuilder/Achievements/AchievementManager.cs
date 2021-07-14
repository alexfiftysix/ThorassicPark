using System.Linq;
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
            foreach (var unlockable in _deck.unlockables.Where(u => !u.isUnlocked))
            {
                unlockable.Test(_toaster);
            }
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (scene.name == "Game")
            {
                _toaster = FindObjectOfType<Toaster>();
            }
        }
    }
}
