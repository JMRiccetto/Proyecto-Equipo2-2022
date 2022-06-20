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
        public UserRegisterHandler(string[] keywords, BaseHandler next) : base(next)
        {
            this.Keywords = keywords;
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
            try
            {
                if (this.State == UserRegisterState.Start)
                {
                    StringBuilder newUser = new StringBuilder("Bienvenido capitán! Te estábamos esperando.\n")
                                                .Append("Antes de poder acceder a tu perfil debes elegir un nombre con el que registrarte,\n")
                                                .Append("asegúrate que sea el mismo que tu user de Telegram.");
                    this.State = UserRegisterState.NickName;
                    response = newUser.ToString();
                    return true;
                }
                else if(this.State == UserRegisterState.NickName)
                {
                    this.Data.NickName = message.Text;
                    this.State = UserRegisterState.Start;
                    UserRegister.Instance.CreateUser(this.Data.NickName);
                    response = "Registro completado, que la suerte esté de tu lado.";
                    return true;
                }
                response = "";
                return false;
            }
            
            catch (Exception e)
            {
                Cancel();
                response = e.Message;
                return true;
            }
        }

        public IHandler Handle(Message message, out string response)
        {
            if (this.CanHandle(message))
            {
                this.InternalHandle(message, out response);
                return this;
            }
            else if (this.Next != null)
            {
                return this.Next.Handle(message, out response);
            }
            else
            {
                response = string.Empty;
                return null;
            }
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