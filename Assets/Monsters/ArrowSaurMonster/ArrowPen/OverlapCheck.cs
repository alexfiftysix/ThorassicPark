using System.Collections.Generic;
using System.Linq;
using Configuration;
using UnityEngine;
using Utilities.Extensions;

namespace Phase_1.Builder.Buildings.ArrowPen
{
    public class OverlapCheck : MonoBehaviour
    {
        private List<GameObject> _overlappingBuildings = new List<GameObject>();
        private const float ClearNullsMaxTime = 1;

        private void Start()
        {
            gameObject.AddTimer(ClearNullsMaxTime, ClearNulls);
        }

        private void ClearNulls()
        {
            _overlappingBuildings = _overlappingBuildings.Where(b => !(b is null)).ToList();
        }

        public bool HasOverlap()
        {
            return _overlappingBuildings.Count > 0;
        }
        
        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Layers.layers[Layer.Building]))
            {
                _overlappingBuildings.Add(other.gameObject);
            }
        }
        
        void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer(Layers.layers[Layer.Building]))
            {
                _overlappingBuildings.Remove(other.gameObject);
            }
        }
    }
}
