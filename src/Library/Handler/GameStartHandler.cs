using System;
using Telegram.Bot.Types;
using Telegram.Bot;
using System.Linq;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "/buscarpartida".
    /// </summary>
    public class GameStartHandler : BaseHandler
    {
        private GameUser user;

        private Match match;

        /// <summary>
        /// Inicializa una nueva instancia de la clase <see cref="GoodByeHandler"/>. Esta clase procesa el mensaje "chau"
        /// y el mensaje "adiós" -un ejemplo de cómo un "handler" puede procesar comandos con sinónimos.
        /// </summary>
        /// <param name="next">El próximo "handler".</param>
        public GameStartHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/buscarpartida"};
            this.user = null;
        }

        /// <summary>
        /// Procesa el mensaje "/buscarpartida" y coloca al jugador en una lista de espera hasta que haya otro con sus mismos settings que desee jugar.
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

                    if (this.user.State == GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No puede acceder al menu mientras está en partida");
                    } 

                    if (this.user.State == GameUser.UserState.Waiting)
                    {
                        throw new InvalidStateException("No puede buscar partida mientras está en cola de espera\n\nIngrese /cancelar para cancelar la busqueda");
                    }

                    if (message.Text.ToLower().Trim() == "/buscarpartida")
                    {
                        this.user.SearchGame();
 
                        if (WaitingList.waitingList.Contains(this.user))
                        {
                            response = "Esperando";
                            return true;
                        }

                        foreach (Match match in Admin.getAdmin().MatchList)
                        {
                            if (match.Players.Contains(this.user.Player))
                            {
                                this.match = match;
                            }
                        }

                        TelegramBotClient bot = ClientBot.GetBot();
 
                        long idPlayer1 = this.match.Players[1].ChatIdPlayer;

                        bot.SendTextMessageAsync(idPlayer1, "Partida creada\n\nPara posicionar un barco ingrese:\n/posicionar-coordenada inicial-dirección\n\nLas direcciones puede ser N S E W\nEl primer barco que coloque será de largo 2 el segundo de largo 3 y el tercero de largo 4\n\nIngrese /rendirse para rendirse");

                        response = "Partida creada\n\nPara posicionar un barco ingrese:\n/posicionar-coordenada inicial-dirección\n\nLas direcciones puede ser N S E W\nEl primer barco que coloque será de largo 2 el segundo de largo 3 y el tercero de largo 4\n\nIngrese /rendirse para rendirse\n\nEs su truno";

                        return true;
                    }
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