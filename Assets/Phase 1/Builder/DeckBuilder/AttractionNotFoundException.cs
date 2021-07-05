using System;
using Phase_1.Builder.Buildings;

namespace Phase_1.Builder.DeckBuilder
{
    public class AttractionNotFoundException : Exception
    {
        public AttractionNotFoundException(string message) : base(message)
        {
        }
    }
}