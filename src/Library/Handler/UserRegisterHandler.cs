using System;
using System.Text;
using Telegram.Bot.Types;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class UserRegisterHandler : BaseHandler
    {
        private GameUser user;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public UserRegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/start"};
            this.user = null;
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {   
            try
            {
                if (this.CanHandle(message))
                {
                    StringBuilder start = new StringBuilder("Bienvenido capitán! Te estábamos esperando.");
                    if(!UserRegister.Instance.UserData.Contains(UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString())))
                    {
                        UserRegister.Instance.CreateUser(message.From.FirstName, message.Chat.Id);
                    }
                    this.user = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());

                    if (this.user.State == GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No puede acceder al menu mientras está en partida");
                    } 
                    if (this.user.State == GameUser.UserState.Waiting)
                    {
                        throw new InvalidStateException("No puede acceder al menu mientras está buscando partida\n\nIngrese /cancelar para cancelar la busqueda");
                    } 
                    
                    start.Append("¿Qué deseas hacer?\n")
                        .Append("/cambiartablero\n")
                        .Append("/bombas\n")
                        .Append("/ataquedoble\n")
                        .Append("/buscarpartida");
                    response = start.ToString();
                    return true;
                }
                response = string.Empty;
                return false;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                Cancel();
                response = e.Message;
                return true;
            }
        }

        /// <summary>
        /// Retorna este "handler" al estado inicial. En los "handler" sin estado no hace nada. Los "handlers" que
        /// procesan varios mensajes cambiando de estado entre mensajes deben sobreescribir este método para volver al
        /// estado inicial.
        /// </summary>
        public override void Cancel()
        {
            this.InternalCancel();
            if (this.Next != null)
            {
                this.Next.Cancel();
            }
        }
    }
}