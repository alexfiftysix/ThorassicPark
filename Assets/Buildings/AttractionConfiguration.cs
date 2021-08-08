using Common.Utilities;
using UnityEngine;

namespace Buildings
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
