using UnityEngine;
using UnityEngine.UI;

namespace GameManagement
{
    public class MoneyBag : MonoBehaviour
    {
        private int _money = 5;
        public Text moneyText;

        /// <summary>
        /// Returns true if the bag has enough money, and deducts that amount if possible
        /// </summary>
        public bool Withdraw(int dollars)
        {
            if (_money >= dollars)
            {
                _money -= dollars;
                SetMoneyText(_money);
                return true;
            }

            return false;
        }

        public void AddMoney(int dollars)
        {
            _money += dollars;
            SetMoneyText(_money);
        }

        private void SetMoneyText(int money)
        {
            moneyText.text = $"${money}";
        }
    }
}
