using System;

namespace NavalBattle
{
    /// <summary>
    /// Excepción que se lanza cuando un jugador introduce una coordenada inválida.
    /// </summary>
    public class InvalidCoordException : Exception
    {
        public InvalidCoordException() : base() { }
        public InvalidCoordException(string message) : base(message) { }
    }
}