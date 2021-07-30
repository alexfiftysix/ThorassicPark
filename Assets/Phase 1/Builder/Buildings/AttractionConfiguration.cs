using UnityEngine;

namespace Phase_1.Builder.Buildings
{
    [CreateAssetMenu(menuName = "Attractions/Configuration")]
    public class AttractionConfiguration : ScriptableObject
    {
        public float prestige;
        public int monsterCount;
        public GameObject monsterPrefab;
    }
}
