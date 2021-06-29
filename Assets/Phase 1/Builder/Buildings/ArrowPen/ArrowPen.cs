using System.Collections.Generic;
using System.Linq;
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
        
        // Ghost
        private List<GameObject> _overlappingBuildings;
        private float _clearNullsMaxTime = 1;
        private float _clearNullsTimePassed = 1;
        private static readonly int GhostShaderColor = Shader.PropertyToID("Color_c9794d5cc0484bfb99bcbf82f83078e6");

        public void Start()
        {
            _overlappingBuildings = new List<GameObject>();
            breakChancePercent = 2;
        }

        public void Update()
        {
            if (!_isBroken && Interval.HasPassed(_moneyTime, _moneyTimePassed, out _moneyTimePassed))
            {
                moneyBag.AddMoney(viewRadius.VisitorCount);
            }
            
            if (Interval.HasPassed(_clearNullsMaxTime, _clearNullsTimePassed, out _clearNullsTimePassed))
            {
                _overlappingBuildings = _overlappingBuildings.Where(b => !(b is null)).ToList();
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

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Building"))
            {
                _overlappingBuildings.Add(other.gameObject);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Building"))
            {
                _overlappingBuildings.Remove(other.gameObject);
            }
        }

        public override bool CanBePlaced()
        {
            var canPlace = _overlappingBuildings.Count == 0;
            return canPlace;
        }

        
        
        public override void SetColor(Color newColor)
        {
            GetComponent<SpriteRenderer>().material.SetColor(GhostShaderColor, newColor);
        }
    }
}
