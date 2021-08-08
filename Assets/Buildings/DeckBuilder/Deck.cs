using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Buildings.DeckBuilder
{
    public class Deck : MonoBehaviour
    {
        public List<Unlockable> unlockables;
        private static Deck _instance;

        private void Awake()
        {
            if (_instance is null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);                
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void UnlockAttraction(string attractionName)
        {
            var firstOrDefault = unlockables.FirstOrDefault(a => a.attraction.name == attractionName);
            if (firstOrDefault is null)
            {
                throw new AttractionNotFoundException($"Attraction {attractionName} not found in deck");
            }

            firstOrDefault.isUnlocked = true;
        }
    }
}
