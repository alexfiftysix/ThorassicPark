using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using Utilities;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class ArrowPen : Attraction
    {
        public GameObject dinosaur;
        [SerializeField] private  int dinosaurCount = 3;

        public List<GameObject> walls; 

        private const int Cost = 1;
        private bool _isBroken = false;

        private float _moneyTime = 5;
        private float _moneyTimePassed;

        public void Start()
        {
            breakChancePercent = 2;
        }

        public void Update()
        {
            if (!_isBroken && Interval.HasPassed(_moneyTime, _moneyTimePassed, out _moneyTimePassed))
            {
                moneyBag.AddMoney(viewRadius.VisitorCount);
            }
        }

        public override int GetCost()
        {
            return Cost;
        }

        public override void Build(MoneyBag newMoneyBag)
        {
            base.Build(newMoneyBag);
            for (var i = 0; i < dinosaurCount; i++)
            {
                Instantiate(dinosaur, transform.position - Vector3.forward, Quaternion.identity);
            }
        }

        public override void Break()
        {
            Destroy(viewRadius);
            foreach (var wall in walls)
            {
                Destroy(wall);
            }

            _isBroken = true;
        }
    }
}
