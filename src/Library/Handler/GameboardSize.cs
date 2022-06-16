using Telegram.Bot.Types;
using System;
}

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class GameboardSizeHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public GameboardSizeHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"CambiarTablero" };
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        int count = 0;
        protected override bool InternalHandle(Message message, out string response)
        {
            if (this.CanHandle(message))
            {
                response = "-> 6 \n -> 7 \n -> 8";
                if (message.Text.Contains ("6"))
                {
                    response = "Tablero cambiado a 6";
                    int Gameboardsize = ((int)char.response);
                    return true;
                }
            }

            response = string.Empty;
            return false;
        }
    }
}