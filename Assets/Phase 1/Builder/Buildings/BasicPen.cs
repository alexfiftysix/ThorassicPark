using System;
using Dinos;
using GameManagement;
using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    public class BasicPen : Attraction
    {
        public GameObject dinosaur;
        public int dinosaurCount = 1; // TODO: Move dino specific code to a new Abstract class? Or just put in in Attraction for now

        public void Start()
        {
            cost = 1;
        }

        public override void Build(MoneyBag newMoneyBag)
        {
            base.Build(newMoneyBag);
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
    }
}
