using System;

namespace NavalBattle
{
    /// <summary>
    /// Excepción que se lanza cuando se quiere ingresar un comando en un momoento en el que no se quiere lanzar.
    /// </summary>
    public class InvalidStateException : Exception
    {
        public InvalidStateException() : base() { }

        public InvalidStateException(string message) : base(message) {}
    }
}