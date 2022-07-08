using System;
using System.Text;
using System.IO;
using Telegram.Bot.Types;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Nito.AsyncEx;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/atacar".
    /// </summary>
    public class CountHandler : BaseHandler
    {
        private GameUser user;

        private Match match;

        TelegramBotClient bot = ClientBot.GetBot();

        /// <summary>
        /// Constructor de CountHandler.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public CountHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/contador"};
        }

        /// <summary>
        /// Procesa el mensaje "/atacar" y ataca el tablero del rival en la coordenada indicada.
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

                    Count shipCounter = new Count();
                    Count waterCounter = new Count();
                    Count bombCounter = new Count();

                    res.Append("\n");

                    printer = new AttackGameboardPrinter();

                    if (Equals(this.user.Player, this.match.Players[0]))
                        {
                            res.Append(shipCounter.AddShipCounter());          
                        }

                    if (Equals(this.user.Player, this.match.Players[0]))
                        {
                            res.Append(waterCounter.AddWaterCounter());          
                        }

                    if (Equals(this.user.Player, this.match.Players[0]))
                        {
                            res.Append(bombCounter.AddBombCounter());          
                        }

                    response = res;
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