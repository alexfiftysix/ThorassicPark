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
        private int _visitorCount = 0;
        private GameManager _gameManager;
        private int MaxVisitorCount => PrestigeToVisitorCount(_gameManager.prestige);

        [SerializeField] private float addVisitorInterval = 0.5f;
        private Timer _addVisitorTimer;

        private static int PrestigeToVisitorCount(float prestige)
        {
            return Convert.ToInt32(Math.Floor(prestige));
        }

        private void Start()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _gameManager.OnParkBreaks += OnParkBreaks;
            _addVisitorTimer = gameObject.AddTimer(addVisitorInterval, SpawnVisitor);
        }

        private void OnParkBreaks(object sender, EventArgs args)
        {
            Destroy(_addVisitorTimer);
        }

        private void SpawnVisitor()
        {
            if (_visitorCount >= MaxVisitorCount) return;

            var visitor = visitorBag.RandomChoice();
            Instantiate(visitor, transform.position, Quaternion.identity);
            _visitorCount += 1;
        }
    }
}