using System;
using Dinos;
using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public class BasicPen : Building
    {
        public GameObject dinosaur;
        public int dinosaurCount = 1;
        private DateTime? _startTime;
        private const int Cost = 1;
        private int _breakTime = 5;

        public void Start()
        {
            _startTime = DateTime.Now;
        }

        public override void Build()
        {
            SpawnDinosaurs();
        }

        public void SpawnDinosaurs()
        {
            var penPosition = transform.position;
            var dinoPosition = penPosition - new Vector3(0, 0, 1);
            for (var i = 0; i < dinosaurCount; i++)
            {
                var dino = Instantiate(dinosaur, dinoPosition, Quaternion.identity);
                dino.GetComponent<Dinosaur>().SetPen(gameObject);
            }
        }
        
        public override bool IsBroken()
        {
            return DateTime.Now - TimeSpan.FromSeconds(_breakTime) > _startTime;
        }

        public override int GetCost()
        {
            return Cost;
        }
    }
}
