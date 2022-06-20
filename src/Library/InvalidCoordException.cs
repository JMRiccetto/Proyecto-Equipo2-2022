using System;

namespace NavalBattle
{
    public class InvalidCoordException : Exception
    {
        public InvalidCoordException() : base() { }
        public InvalidCoordException(string message) : base(message) { }
    }
}