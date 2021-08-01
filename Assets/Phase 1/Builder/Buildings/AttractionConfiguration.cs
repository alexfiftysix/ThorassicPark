using UnityEngine;
using Utilities;

namespace Phase_1.Builder.Buildings
{
    [CreateAssetMenu(menuName = "Attractions/Configuration")]
    public class AttractionConfiguration : ScriptableObject
    {
        public float prestige;
        public int monsterCount;
        public GameObject monsterPrefab;
        public RangedFloat timeToBecomeDamaged;
    }
}
