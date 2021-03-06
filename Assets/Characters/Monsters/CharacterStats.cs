using Common.Audio;
using UnityEngine;

namespace Characters.Monsters
{
    [CreateAssetMenu(menuName = "Character/Stats")]
    public class CharacterStats : ScriptableObject
    {
        public int maxHealth;

        public int meleeDamage;
        public AudioEvent meleeSound;

        public float movementSpeed;
        public float turnSpeed;

        public float viewRadius = 1;
        public float touchRadius = 0.25f;
    }
}