using System.Collections.Generic;
using UnityEngine;
using Utilities;

namespace Visitors
{
    public class VisitorCentre : MonoBehaviour
    {
        public float spawnChance;
        public List<GameObject> visitorBag;

        // Update is called once per frame
        void Update()
        {
            if (MyRandom.CoinFlip(spawnChance))
            {
                var visitor  =visitorBag.RandomChoice();
                Instantiate(visitor, transform.position, Quaternion.identity);
            }
        }
    }
}
