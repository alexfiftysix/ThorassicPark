using Phase_1.Builder.DeckBuilder;
using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            if (MyStatistics.moneyEarned >= 10) // TODO: Wrap this up in some kind of Achievements class
            {
                _deck.UnlockAttraction("ArrowPenPink");
            }
        }

        public void PlayAgain()
        {
            SceneManager.LoadScene(1); // TODO: Don't use magic numbers
        }

        public void Quit()
        {
            Debug.Log("QUIT");
            Application.Quit();
        }
    }
}