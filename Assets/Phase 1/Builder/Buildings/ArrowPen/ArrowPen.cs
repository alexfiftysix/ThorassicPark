using GameManagement;
using UnityEngine;
using Utilities;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class ArrowPen : Attraction
    {
        private const float MoneyTime = 5; // TODO: Move this to its own script
        private float _moneyTimePassed;

        private void Start()
        {
            breakChancePercent = 2;
            isBroken = false;
        }

        public void Update()
        {
            if (!isBroken && Interval.HasPassed(MoneyTime, _moneyTimePassed, out _moneyTimePassed))
            {
                moneyBag.AddMoney(viewRadius.VisitorCount);
            }
        }
    }
}
