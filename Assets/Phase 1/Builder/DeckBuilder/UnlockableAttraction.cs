using System;
using Phase_1.Builder.Buildings;

namespace Phase_1.Builder.DeckBuilder
{
    [Serializable]
    public class UnlockableAttraction
    {
        public Attraction attraction;
        public bool isUnlocked;
    }
}