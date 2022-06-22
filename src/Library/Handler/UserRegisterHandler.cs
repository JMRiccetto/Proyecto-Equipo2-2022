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
        public UserRegisterState State;

        public UserRegisterData Data;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public UserRegisterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/start"};
            this.State = UserRegisterState.Start;
            this.Next = next;
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            if (this.CanHandle(message))
            {
                StringBuilder start = new StringBuilder("Bienvenido capitán! Te estábamos esperando.");
                if(!UserRegister.Instance.UserData.Contains(UserRegister.Instance.GetUserByNickName(message)))
                {
                    UserRegister.Instance.CreateUser(message.From.FirstName);
                }
                start.Append("Escriba /menu para ver las opciones disponibles.");
                response = start.ToString();
                return true;
            }
            response = string.Empty;
            return false;
        }

        protected override void InternalCancel()
        {
            this.State = UserRegisterState.Start;
            this.Data = new UserRegisterData();
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
            Start,

            NickName,
        }

        public class UserRegisterData
        {
            public string NickName { get; set; }
        }
    }
}