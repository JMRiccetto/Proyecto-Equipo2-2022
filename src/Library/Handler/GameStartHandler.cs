using System;
using System.Text;
using Telegram.Bot.Types;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class GameStartHandler : BaseHandler
    {
        public GameStartState State;

        public GameUser User;


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public GameStartHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/buscarpartida"};
            this.State = GameStartState.Start;
            this.User = null;
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
                if (this.State == GameStartState.Start && this.CanHandle(message))
                {
                    this.User = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());
                    //response = "Vuelva con vida capitán, es una orden.";

                    if (message.Text.ToLower().Trim() == "/buscarpartida")
                    {
                        User.SearchGame();
 
                        if(WaitingList.waitingList.Contains(this.User))
                        {   
                            response = "Esperando";
                            return true;
                        }
                        response = "Partida creada\n para posicionar un barco ingrese: /posicionar coordenada inicial direccion \n Las direcciones puede ser N S E W \n El primer barco que cree sera de largo 2 el segundo de largo 3 y el tercero de largo 4";
                        
                        return true;
                    }
                }
                response = "";
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

        protected override void InternalCancel()
        {
            this.State = GameStartState.Start;
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

        public enum GameStartState
        {
            Start,
            Waiting,
            InGame,    
        }

        public class GameStartData
        {
        }
    }
} 