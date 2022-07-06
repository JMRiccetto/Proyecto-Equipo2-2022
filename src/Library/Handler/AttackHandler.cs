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
    public class AttackHandler : BaseHandler
    {
        private GameUser user;

        private Match match;

        TelegramBotClient bot = ClientBot.GetBot();

        /// <summary>
        /// Constructor de AttackHandler.
        /// </summary>
        /// <param name="next"></param>
        /// <returns></returns>
        public AttackHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/atacar"};
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

                    if (this.user.Player.Turn)
                    {
                        string[] input = message.Text.Split("-");

                        string attackCoordStr = input[1].ToUpper();

                        string res = "hola";

                        long idPlayer0 = this.match.Players[0].ChatIdPlayer;

                        long idPlayer1 = this.match.Players[1].ChatIdPlayer;

                        if (Equals(this.user.Player, this.match.Players[0]))
                        {
                            res = this.user.Player.Attack(attackCoordStr, this.match.Players[1].Gameboard);
                            AsyncContext.Run(() => SendImage(message));
                            AsyncContext.Run(() => SendVoice(message));
                        }
                        else
                        {
                            res = this.user.Player.Attack(attackCoordStr, this.match.Players[0].Gameboard);
                            AsyncContext.Run(() => SendImage(message));
                            AsyncContext.Run(() => SendVoice(message));
                        }

                        this.match.Players[0].ChangeTurn();

                        this.match.Players[1].ChangeTurn();

                        if (res == "Fin")
                        {
                            this.bot.SendTextMessageAsync(idPlayer0, $"La partida finalizó. {this.user.NickName} es el/la ganador/a");

                            this.bot.SendTextMessageAsync(idPlayer1, $"La partida finalizó. {this.user.NickName} es el/la ganador/a");

                            Admin.getAdmin().MatchList.Remove(this.match);

                            GameUser user1 = UserRegister.Instance.GetUserById(idPlayer0);

                            GameUser user2 = UserRegister.Instance.GetUserById(idPlayer1);

                            user1.State = GameUser.UserState.NotInGame;

                            user2.State = GameUser.UserState.NotInGame;

                            response = string.Empty;

                            return true;
                        }

                        if (this.match.Players[0].Turn)
                        {
                            this.bot.SendTextMessageAsync(idPlayer0, "Es su turno");
                        }
                        else
                        {
                            long id = this.match.Players[1].ChatIdPlayer;

                            this.bot.SendTextMessageAsync(idPlayer1, "Es su turno");
                        }

                        response = res+"\n\nIngrese /vertableros para ver sus tableros\n\nIngrese /rendirse para rendirse";

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
            catch (NullReferenceException ne)
            {
                response = "Ingrese /start para acceder al menu de opciones.";

                return true;
            }
            catch (IndexOutOfRangeException re)
            {
                response = "Comando no válido. Inténtelo de nuevo";

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
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

        /// <summary>
        /// Determina si este "handler" puede procesar el mensaje. En la clase base se utiliza el array
        /// <see cref="BaseHandler.Keywords"/> para buscar el texto en el mensaje ignorando mayúsculas y minúsculas. Las
        /// clases sucesores pueden sobreescribir este método para proveer otro mecanismo para determina si procesan o no
        /// un mensaje.
        /// </summary>
        /// <param name="message">El mensaje a procesar.</param>
        /// <returns>true si el mensaje puede ser pocesado; false en caso contrario.</returns>
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

        /// <summary>
        /// Método para mandar imágenes por medio del bot.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendImage(Message message)
        {
            await this.bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);
            string path = @"..\..\Assets\barco.jpg";
            this.bot.SendPhotoAsync(message.Chat.Id, path);
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = path.Split(Path.DirectorySeparatorChar).Last();
            await this.bot.SendPhotoAsync(
            chatId: message.Chat.Id,
            photo: new InputOnlineFile(fileStream, fileName));
        }

        /// <summary>
        /// Método para mandar audio por medio del bot.
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendVoice(Message message)
        {
            await this.bot.SendChatActionAsync(message.Chat.Id, ChatAction.UploadVoice);
            string path = @"..\..\Assets\CannonBall.mp3";
            this.bot.SendVoiceAsync(message.Chat.Id, path);
            using var fileStream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = path.Split(Path.DirectorySeparatorChar).Last();
            await this.bot.SendVoiceAsync(
            chatId: message.Chat.Id,
            voice: new InputOnlineFile(fileStream, fileName));
        }
    }
}
