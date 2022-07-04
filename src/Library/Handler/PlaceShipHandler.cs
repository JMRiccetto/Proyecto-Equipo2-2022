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
        private GameUser user;

        private Match match;

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

                    if (this.user.Player.Turn)
                    {
                        string[] input = message.Text.Split("-");

                        string initialCoord = input[1].ToUpper();

                        string direction = input[2].ToUpper();

                        this.user.Player.PlaceShip(initialCoord, direction);

                        this.match.Players[0].ChangeTurn();

                        this.match.Players[1].ChangeTurn();

                        long idPlayer0 = this.match.Players[0].ChatIdPlayer;
                        
                        long idPlayer1 = this.match.Players[1].ChatIdPlayer;

                        TelegramBotClient bot = ClientBot.GetBot();

                        bot.SendTextMessageAsync(message.Chat.Id, "Barco posicionado correctamente\n\nIngrese /vertableros para ver sus tableros");

                        if ((match.Players[0].CounterShipLength > 4) && (match.Players[1].CounterShipLength > 4))
                        {
                            bot.SendTextMessageAsync(idPlayer1, "La fase de posicionamiento terminó\n\nComienza la fase de ataque, ingrese /atacar-coordenada a atacar\n\nIngrese /rendirse para rendirse");
                            bot.SendTextMessageAsync(idPlayer0, "La fase de posicionamiento terminó\n\nComienza la fase de ataque, ingrese /atacar-coordenada a atacar\n\nIngrese /rendirse para rendirse");
                        }

                        if(match.Players[0].Turn)
                        {            
                            bot.SendTextMessageAsync(idPlayer0, "\n\nEs su turno");
                        }
                        else
                        {
                            bot.SendTextMessageAsync(idPlayer1, "\n\nEs su turno");
                        }
                    
                        response = "";

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
            catch(NullReferenceException ne)
            {
                response = "Ingrese /start para acceder al menu de opciones.";

                return true;
            }
            catch(IndexOutOfRangeException re)
            {
                response = "Comando no válido. Inténtelo de nuevo";

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
            
            string[] input = message.Text.Split("-");
            
            if (this.Keywords.Contains(input[0]))
            {
                return true;
            }
            
            return false;
        }
    }
}