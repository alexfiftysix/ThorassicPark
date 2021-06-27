using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phase_1.Builder.Buildings
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

        public void Start()
        {
            _startTime = Time.time;
        }

        public void Update()
        {
            if (!_isBroken && Time.time - _startTime > breakTime)
            {
                _isBroken = true;
                BreakWalls();
            }
        }

        public override int GetCost()
        {
            return Cost;
        }

        public override void Build()
        {
            for (var i = 0; i < dinosaurCount; i++)
            {
                Instantiate(dinosaur, transform.position - Vector3.forward, Quaternion.identity);
            }
        }

        private void BreakWalls()
        {
            foreach (var wall in walls)
            {
                Destroy(wall);
            }
        }
    }
}
