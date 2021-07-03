using System.Collections.Generic;
using UnityEngine;

namespace Phase_1.Builder.DeckBuilder
{
    public class CardStore : MonoBehaviour
    {
        public static readonly List<string> UnlockedAttractions = new List<string>()
        {
            "ArrowPen", // TODO: Don't use strings here
            "ArrowPenBlue",
            // "ArrowPenPink",
        };

        public static void UnlockCard(string name)
        {
            if (!UnlockedAttractions.Contains(name))
            {
                UnlockedAttractions.Add(name);
            }
        }
    }
}
