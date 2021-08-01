using Common.Audio;
using UnityEngine;

namespace Monsters
{
    [CreateAssetMenu(menuName = "Monsters/Stats")]
    public class MonsterStats : ScriptableObject
    {
        public int maxHealth;

        public int meleeDamage;
        public AudioEvent meleeSound;

        public float movementSpeed;
        public float turnSpeed;
    }
}