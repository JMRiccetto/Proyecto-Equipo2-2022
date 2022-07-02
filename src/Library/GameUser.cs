using System.Text.Json;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace NavalBattle
{
    public class GameUser : IJsonConvertible
    {   
        private long chatId;

        public long ChatId
        {
            get
            {
                return this.chatId;
            }
        }
        private bool bombs = false;

        public bool Bombs
        {
            get
            {
                return bombs;
            }
            set
            {
                this.bombs = value;
            }
        }

        private bool doubleAttack = false;

        public bool DoubleAttack
        {
            get
            {
                return doubleAttack;
            }
            set
            {
                this.doubleAttack = value;
            }
        }
        private int gameboardSide = 6;

        public int GameboardSide
        {
            get
            {
                return gameboardSide;
            }
            set
            {
                this.gameboardSide = value;
            }
        }
        private string nickName;

        public string NickName
        {
            get
            {
                return this.nickName;
            }
            private set
            {
                this.nickName = this.NickName != null ? this.NickName : value;
            }
        }

        
        private Player player;


        public GameUser(string nickName, long aChatId)
        {
            this.chatId = aChatId;
            this.NickName = nickName;
        }

        public Player Player
        {
            get
            {
                return this.player;
            }
            set
            {
                this.player = value;
            }
        }
        
        /// <summary>
        /// El usuuario busca partida eligiendo las caracteristicas con las que quiere jugar.
        /// </summary>
        /// <param name="gameboardSide"></param>
        /// <param name="bombs"></param>
        /// <param name="doubleAttack"></param>
        public void SearchGame() 
        {
            Admin.getAdmin().AddToWaitingList(this);
        }


        public string ConvertToJson(JsonSerializerOptions options)
        {
            JsonSerializerOptions option = new ()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, option);
        }
    }
}