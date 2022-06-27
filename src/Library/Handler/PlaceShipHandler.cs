using System;
using System.Text;
using Telegram.Bot.Types;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class PlaceShipHandler : BaseHandler
    {
        public MatchState State;

        public GameUser User;

        


        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public PlaceShipHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/posicionar"};

            //this.State = 
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
                    this.User = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());

                    if (message.Text.ToLower().Trim() == "/posicionar")
                    {
                        response = "ingrese coordenada inicial";
                        return true;
                    }

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

        public enum UserRegisterState
        {
        }

        public class UserRegisterData
        {
            public string NickName { get; set; }
        }

        public enum MatchState
        {
            Start,
            positioning,
            InGame,    
        }
    }
}