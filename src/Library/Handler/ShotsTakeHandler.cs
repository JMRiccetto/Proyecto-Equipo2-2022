using System;
using System.Text;
using Telegram.Bot.Types;
using System.Linq;
using System.Collections.Generic;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/vertableros".
    /// </summary>
    public class ShotsTakeHandler : BaseHandler
    {
        int t;
        private GameUser user;

        private Match match;

        /// <summary>
        /// Constructor de PrintGameBoardHandler.
        /// </summary>
        /// <param name="next">Próximo Handler.</param>
        /// <returns>PrintGameboardHandler.</returns>
        public ShotsTakeHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/verDisparosTocado"};
        }

        /// <summary>
        /// Procesa el mensaje "/vertableros" y le muestra al usuario el tablero con sus barcos y el tablero donde ha atacado.
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

                    if (this.user.State != GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No puede realizar esta acción en este momento");
                    }

                    foreach (Match match in Admin.getAdmin().MatchList)
                    {
                        if (match.Players.Contains(this.user.Player))
                        {
                            this.match = match;
                        }
                    }

                    if (message.Text.Equals("/verDisparosTocados"))
                    {
                        int t = 0;
                        t =+ this.match.Players[1].Gameboard.Tcount;          
                        t =+ this.match.Players[0].Gameboard.Tcount;
                    }

                    response = "el numero de disparos tocados es: " + t.ToString();
                    return true;
                }

                response = string.Empty;
                return false;
            }
            catch (NullReferenceException ne)
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

        protected override bool CanHandle(Message message)
        {
            if (this.Keywords == null || this.Keywords.Length == 0)
            {
                throw new InvalidOperationException("No hay palabras clave que puedan ser procesadas");
            }

            string[] input = message.Text.Split(" ");

            if (this.Keywords.Contains(input[0]))
            {
                return true;
            }

            return false;
        }
    }
}