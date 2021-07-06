using System;
using System.Collections.Generic;
using GameManagement;
using UnityEngine;
using Utilities;
using Utilities.Extensions;

namespace Visitors
{
    public class VisitorCentre : MonoBehaviour
    {
        public float spawnChance;
        public List<GameObject> visitorBag;
        private bool _parkIsBroken = false;

        private void Start()
        {
            FindObjectOfType<GameManager>().OnParkBreaks += OnParkBreaks;
        }

        private void OnParkBreaks(object sender, EventArgs args)
        {
            _parkIsBroken = true;
        }

        // Update is called once per frame
        private void Update()
        {
            if (_parkIsBroken) return;

            if (MyRandom.CoinFlip(spawnChance))
            {
                var visitor = visitorBag.RandomChoice();
                Instantiate(visitor, transform.position, Quaternion.identity);
            }
        }
    }
}