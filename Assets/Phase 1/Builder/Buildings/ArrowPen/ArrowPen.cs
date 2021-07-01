using GameManagement;
using UnityEngine;
using Utilities;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class ArrowPen : Attraction
    {
        public GameObject dinosaur;
        [SerializeField] private  int dinosaurCount = 3;

        private const float MoneyTime = 5; // TODO: Move this to its own script
        private float _moneyTimePassed;
        
        // Ghost
        private static readonly int GhostShaderColor = Shader.PropertyToID("Color_c9794d5cc0484bfb99bcbf82f83078e6");

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

        public override void Build(MoneyBag newMoneyBag)
        {
            base.Build(newMoneyBag);
            for (var i = 0; i < dinosaurCount; i++)
            {
                Instantiate(dinosaur, transform.position - Vector3.forward, Quaternion.identity);
            }
        }
    }
}
