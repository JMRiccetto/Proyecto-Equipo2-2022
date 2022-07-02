using System;
using System.Text;
using Telegram.Bot.Types;
using System.Linq;
using System.Collections.Generic;
using Telegram.Bot;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "chau".
    /// </summary>
    public class PlaceShipHandler : BaseHandler
    {
        public MatchState State;

        public GameUser User;

        public Match match;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public PlaceShipHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/posicionar"};

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

                    foreach (Match match in Admin.getAdmin().MatchList)
                    {
                        if (match.Players.Contains(this.User.Player))
                        {
                            this.match = match;
                        }
                    }

                    if (this.User.Player.Turn)
                    {
                        string[] input = message.Text.Split(" ");

                        string initialCoord = input[1];

                        string direction = input[2];

                        this.User.Player.PlaceShip(initialCoord, direction);

                        this.match.Players[0].ChangeTurn();

                        this.match.Players[1].ChangeTurn();

                        TelegramBotClient bot = ClientBot.GetBot();

                        if(match.Players[0].Turn)
                        {
                            long id = this.match.Players[0].ChatIdPlayer;
                        
                            bot.SendTextMessageAsync(id, "Es su turno");
                        }
                        else
                        {
                            long id = this.match.Players[1].ChatIdPlayer;
                        
                            bot.SendTextMessageAsync(id, "Es su turno");
                        }
                    
                        response = "Barco posicionado correctamente";

                        return true;
                    }
                    else
                    {
                        response = "Espere su turno";

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

        public enum MatchState
        {
            Start,
            positioning,
            InGame,    
        }
    }
}