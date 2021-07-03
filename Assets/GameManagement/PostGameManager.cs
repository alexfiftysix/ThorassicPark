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
        
        // Start is called before the first frame update
        void Start()
        {
            winLoseText.text = MyStatistics.WonLastGame ? "You won!" : "You lost";
            moneyText.text = $"${MyStatistics.MoneyEarned}";
            
            // Achievements
            if (!MyStatistics.WonLastGame) return;
            if (MyStatistics.MoneyEarned >= 10)
            {
                CardStore.UnlockCard("ArrowPenPink");
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
