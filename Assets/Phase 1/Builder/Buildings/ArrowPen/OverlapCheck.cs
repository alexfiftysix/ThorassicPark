using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class OverlapCheck : MonoBehaviour
    {
        private List<GameObject> _overlappingBuildings;
        private float _clearNullsMaxTime = 1;
        private float _clearNullsTimePassed = 1;
    
        // Start is called before the first frame update
        void Start()
        {
            _overlappingBuildings = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Interval.HasPassed(_clearNullsMaxTime, _clearNullsTimePassed, out _clearNullsTimePassed))
            {
                _overlappingBuildings = _overlappingBuildings.Where(b => !(b is null)).ToList();
            }
        }

        public bool HasOverlap()
        {
            return _overlappingBuildings.Count > 0;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            var line = other.gameObject.layer == LayerMask.NameToLayer("LineOfSight");
            
            if (other.gameObject.layer == LayerMask.NameToLayer("Building"))
            {
                _overlappingBuildings.Add(other.gameObject);
                if (line) Debug.Log("LINE");
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Building"))
            {
                _overlappingBuildings.Remove(other.gameObject);
            }
        }
    }
}
