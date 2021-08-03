using Phase_1.Builder.DeckBuilder;
using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Scene = Configuration.Scene;

namespace GameManagement
{
    public class PostGameManager : MonoBehaviour
    {
        public TextMeshProUGUI winLoseText;
        public TextMeshProUGUI moneyText;
        public TextMeshProUGUI visitorsSavedText;
        private Deck _deck;

        // Start is called before the first frame update
        void Start()
        {
            winLoseText.text = MyStatistics.wonLastGame ? "You won!" : "You lost";
            moneyText.text = $"${MyStatistics.moneyEarned}";
            visitorsSavedText.text = $"{MyStatistics.visitorsSaved}";
            _deck = FindObjectOfType<Deck>();

            // Achievements
            if (!MyStatistics.wonLastGame) return;
            // TODO: You have achievements already, this seems like a mistake
            if (MyStatistics.moneyEarned >= 10) // TODO: Wrap this up in some kind of Achievements class
            {
                _deck.UnlockAttraction("ArrowPenPink");
            }
            if (MyStatistics.visitorsSaved >= 10) // TODO: Wrap this up in some kind of Achievements class
            {
                _deck.UnlockAttraction("ZombiePen");
            }
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(Configuration.Configuration.Scenes[Scene.DeckBuild]);
        }

        public void Quit()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}