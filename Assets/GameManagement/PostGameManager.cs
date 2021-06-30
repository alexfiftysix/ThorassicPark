using Statistics;
using TMPro;
using UnityEngine;

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
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
