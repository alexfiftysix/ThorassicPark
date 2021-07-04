using Utilities;
using Utilities.Extensions;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class ArrowPen : Attraction
    {
        private const float MoneyTime = 5;
        private Timer _moneyTimer;

        private void Start()
        {
            breakChancePercent = 2;
            isBroken = false;
            _moneyTimer = gameObject.AddTimer(MoneyTime, AddMoney);
        }

        private void AddMoney()
        {
            // TODO: Move this money logic to its own script
            moneyBag.AddMoney(viewRadius.VisitorCount);
        }

        protected override void Break()
        {
            base.Break();
            Destroy(_moneyTimer);
        }
    }
}
