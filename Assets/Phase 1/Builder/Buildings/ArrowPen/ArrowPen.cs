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

        [SerializeField] private int breakTime = 5;
        private const int Cost = 1;
        private float _startTime;
        private bool _isBroken = false;

        private float _moneyTime = 5;
        private float _moneyTimePassed;

        public void Start()
        {
            _startTime = Time.time;
        }

        public void Update()
        {
            if (!_isBroken && Time.time - _startTime > breakTime)
            {
                Break();
            }

            if (!_isBroken && Interval.HasPassed(_moneyTime, _moneyTimePassed, out _moneyTimePassed))
            {
                Debug.Log("TICK");
                Debug.Log($"Count: {viewRadius.VisitorCount}");
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

        private void Break()
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
