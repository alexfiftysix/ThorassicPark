using Statistics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameManagement
{
    public class MoneyBag : MonoBehaviour
    {
        public TextMeshProUGUI moneyText;

        /// <summary>
        /// Returns true if the bag has enough money, and deducts that amount if possible
        /// </summary>
        public bool Withdraw(int dollars)
        {
            if (MyStatistics.moneyEarned >= dollars)
            {
                MyStatistics.moneyEarned -= dollars;
                SetMoneyText(MyStatistics.moneyEarned);
                return true;
            }

            return false;
        }

        public void AddMoney(int dollars)
        {
            MyStatistics.moneyEarned += dollars;
            SetMoneyText(MyStatistics.moneyEarned);
        }

        private void SetMoneyText(int money)
        {
            moneyText.text = $"${money}";
        }
    }
}
