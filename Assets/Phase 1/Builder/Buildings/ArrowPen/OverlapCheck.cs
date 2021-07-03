using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utilities;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class OverlapCheck : MonoBehaviour
    {
        private List<GameObject> _overlappingBuildings = new List<GameObject>();
        private float _clearNullsMaxTime = 1;
        private float _clearNullsTimePassed = 1;
    
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
    }
}
