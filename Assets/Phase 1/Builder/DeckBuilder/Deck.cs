using System.Collections.Generic;
using System.Linq;
using Phase_1.Builder.Buildings;
using UnityEngine;

namespace Phase_1.Builder.DeckBuilder
{
    public class Deck : MonoBehaviour
    {
        private const int MaxChosen = 2;
        public List<AttractionCard> chosen = new List<AttractionCard>(MaxChosen);

        public bool AttractionIsChosen(Attraction attraction)
        {
            return chosen.Exists(c => !(c is null) && !c.isEmpty && c.attraction.name == attraction.name); // TODO: Checking on name is bad
        }

        public void DeSelectAttraction(Attraction attraction)
        {
            if (AttractionIsChosen(attraction))
            {
                chosen.First(c => !c.isEmpty && c.attraction.name == attraction.name).SetAttraction(null); // TODO: Checking on name is bad
            }
        }

        private bool IsFull()
        {
            return chosen.All(c => !c.isEmpty);
        }

        /// <summary>
        /// Add attraction to your deck
        /// </summary>
        /// <param name="attraction"></param>
        /// <returns>True if attraction was selected</returns>
        public bool ChooseAttraction(Attraction attraction)
        {
            if (IsFull()) return false;

            chosen.First(c => c.isEmpty).SetAttraction(attraction);
            ChosenCards.Attractions = chosen;
            return true;
        }
    }
}
