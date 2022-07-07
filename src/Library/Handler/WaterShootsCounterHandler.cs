using System;
using System.Text;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/disparosagua".
    /// </summary>
    public class WaterShootsCounterHandler : BaseHandler
    {
        public GameUser User { get; set; }

        public Match Match { get; set; }

        public int WaterShootsCounter { get; set; }

        /// <summary>
        /// Constructor de WaterShootsHandler.
        /// </summary>
        /// <param name="next">El próximo handler.</param>
        /// <returns></returns>
        public WaterShootsCounterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/disparosagua"};
            this.User = null;
        }

        /// <summary>
        /// Procesa el mensaje "/disparosagua", terminando la partida y dándole la victoria al otro jugador.
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

                    if (this.User.State != GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No es posible realizar esta acción en este momento");
                    }

                    foreach (Match match in Admin.getAdmin().MatchList)
                    {
                        if (match.Players.Contains(this.User.Player))
                        {
                            this.Match = match;
                        }
                    }

                    this.WaterShootsCounter = this.Match.Players[0].WaterShoots() + this.Match.Players[1].WaterShoots();

                    response = $"El total de disparos al agua ha sido de {this.WaterShootsCounter}";

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
                this.Cancel();
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