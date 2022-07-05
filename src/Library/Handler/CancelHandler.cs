using System;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/cancelar".
    /// </summary>
    public class CancelHandler : BaseHandler
    {
        private GameUser user;
        

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public CancelHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/cancelar"};
            this.user = null;
        }

        /// <summary>
        /// Procesa el mensaje "/cancelar" y remueve al usuario de la lista de espera.
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
                    this.user = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());

                    if(this.user.State != GameUser.UserState.Waiting)
                    {
                        throw new InvalidStateException("No es posible realizar esta acción en este momento");
                    }
                    WaitingList.waitingList.Remove(this.user);

                    this.user.State = GameUser.UserState.NotInGame;

                    response = "Busqueda cancelada\n\nIngrese /start para ver el menu de opciones";

                    return true;
                }
                response = "";
                return false;
            }
            catch(NullReferenceException ne)
            {
                response = "Ingrese /start para acceder al menu de opciones.";

                return true;
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