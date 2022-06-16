using Telegram.Bot.Types;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class NickNameHandler : BaseHandler
    {
        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public NickNameHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] { "/apodo", "/cambiar apodo" };
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
                response = "Ingrese un apodo:";
                User user = new User(message.Text); //se crea una instancia de la clase User con el nickname del usuario
                

                if (count == 0)
                {
                    response = "Buen apodo!";
                    count++;
                }
                else
                {
                    response = "Apodo cambiado";
                }
                return true;
            }

            response = string.Empty;
            return false;
        }
    }
}