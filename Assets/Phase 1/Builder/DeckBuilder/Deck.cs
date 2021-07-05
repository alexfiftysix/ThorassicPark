using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder.Buildings;
using UnityEngine;

namespace Phase_1.Builder.DeckBuilder
{
    public class Deck : MonoBehaviour
    {
        public List<UnlockableAttraction> attractions;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        public void UnlockAttraction(string attractionName)
        {
            var firstOrDefault = attractions.FirstOrDefault(a => a.attraction.name == attractionName);
            if (firstOrDefault is null)
            {
                throw new AttractionNotFoundException($"Attraction {attractionName} not found in deck");
            }

            firstOrDefault.isUnlocked = true;
        }

        public bool IsUnlocked(Attraction attraction)
        {
            // TODO: String comparison bad
            return attractions.Exists(a => a.attraction.name == attraction.name && a.isUnlocked);
        }
    }
}
