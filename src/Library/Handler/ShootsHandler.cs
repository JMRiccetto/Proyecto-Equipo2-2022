using System;
using Telegram.Bot.Types;
using System.Collections.Generic;
using System.Linq;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/cancelar".
    /// </summary>
    public class ShootsHandler : BaseHandler
    {
        private GameUser user;

        private Match match;

        /// <summary>
        /// Constructor de ShootsHandler.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public ShootsHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/disparosagua", "/disparosbarcos"};
            this.user = null;
        }

        /// <summary>
        /// Procesa el mensaje "/disparosagua" o "/disparosbarcos" y en devuelve la cantidad de disparos
        /// realizados al agua o a los barcos dependiendo del comando respectivamente.
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
                        throw new InvalidStateException("No es posible realizar esta acción en este momento");
                    }

                    foreach (Match match in Admin.getAdmin().MatchList)
                    {
                        if (match.Players.Contains(this.user.Player))
                        {
                            this.match = match;
                        }
                    }

                    if (message.Text.ToLower().Trim() == "/disparosagua")
                    {
                        //Sumo la cantidad de disparos al agua de los dos jugadores.
                        int res = (this.match.Players[0].Disparos.WaterShoots + this.match.Players[1].Disparos.WaterShoots);
                        response = "La cantidad de disparos al agua es: " + res.ToString();
                    }
                    if (message.Text.ToLower().Trim() == "/disparostocado")
                    {
                        //Sumo la cantidad de disparos a barcos de los dos jugadores
                        int res = (this.match.Players[0].Disparos.ShipShoots + this.match.Players[1].Disparos.ShipShoots);
                        response = "La cantidad de disparos a barcos es: " + res.ToString();
                    }
                    response = "";
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
    }
}