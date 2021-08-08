using Common.Audio;
using UnityEngine;

namespace Monsters
{
    [CreateAssetMenu(menuName = "Character/Stats")]
    public class CharacterStats : ScriptableObject
    {
        public int maxHealth;

        public int meleeDamage;
        public AudioEvent meleeSound;

        public float movementSpeed;
        public float turnSpeed;
    }
}