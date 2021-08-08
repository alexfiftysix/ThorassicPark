using System;

namespace Buildings.DeckBuilder
{
    public class AttractionNotFoundException : Exception
    {
        public AttractionNotFoundException(string message) : base(message)
        {
        }
    }
}