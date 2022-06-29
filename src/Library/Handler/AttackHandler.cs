using System;
using System.Text;
using Telegram.Bot.Types;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class AttackHandler : BaseHandler
    {
        public AttackState State;

        public Player Player;

        public Match Match;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public AttackHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/attack"};
            this.Player = null;
            this.Match = null;
        }

        /// <summary>
        /// Procesa el mensaje "chau" y retorna true; retorna false en caso contrario.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <param name="response">La respuesta al mensaje procesado.</param>
        /// <returns>true si el mensaje fue procesado; false en caso contrario.</returns>
        protected override bool InternalHandle(Message message, out string response)
        {
            if (this.CanHandle(message) && this.State == AttackState.Start)
            {   
                this.Player = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString()).player;
                foreach (Match match in )
            }
            else if (this.State == AttackState.Attack)
            {
                foreach (Player player in this.Ma)
            }
            response = string.Empty;
            return false;
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

        protected virtual bool CanHandle(Message message)
        {
            // Cuando no hay palabras clave este método debe ser sobreescrito por las clases sucesoras y por lo tanto
            // este método no debería haberse invocado.
            if (this.Keywords == null || this.Keywords.Length == 0)
            {
                throw new InvalidOperationException("No hay palabras clave que puedan ser procesadas");
            }

            return this.Keywords.Any(s => message.Text.Equals(s, StringComparison.InvariantCultureIgnoreCase));
        }

        public enum AttackState
        {
            Start,
            Attack,
        }

        public class UserRegisterData
        {
            public string NickName { get; set; }
        }
    }
}