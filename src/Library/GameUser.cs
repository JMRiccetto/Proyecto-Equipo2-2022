using System.Text.Json;
using System.Text.Json.Serialization;
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

        private bool bombs = false;

        private bool doubleAttack = false;

        private int gameboardSide = 6;

        private string nickName;

        private Player player;

        private UserState state = UserState.NotInGame;

        //[JsonConstructor]
        public GameUser(string nickName, long aChatId)
        {
            this.chatId = aChatId;
            this.nickName = nickName;
        }

        /* public GameUser(string json)
        {
            this.LoadFromJson(json);
        } */

        public long ChatId
        {
            get
            {
                return this.chatId;
            }
            set
            {
                this.chatId = value;
            }
        }

        public UserState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.state = value;
            }
        }

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
        /// Estado del usuario. Se utiliza para controlar excepciones y comandos no validos en diferentes momentos.
        /// Solo se controla si el usuario está en partida o no, porque los demas estdos se controlan en Gameboard.
        /// Por ejemplo: 
        ///     que un jugador no pueda atacar en la fase de posicionamiento.
        ///     que un jugador no pueda posicionar barcos en la fase de ataque.
        /// </summary>
        public enum UserState
        {   
            NotInGame,
            Waiting,
            InGame,

        }
        
        /// <summary>
        /// El usuuario busca partida eligiendo las caracteristicas con las que quiere jugar.
        /// </summary>
        /// <param name="gameboardSide"></param>
        /// <param name="bombs"></param>
        /// <param name="doubleAttack"></param>
        public void SearchGame() 
        {
            this.state = GameUser.UserState.Waiting;
            Admin.getAdmin().AddToWaitingList(this);
        }


        public string ConvertToJson()
        {
            JsonSerializerOptions option = new()
            {
                ReferenceHandler = MyReferenceHandler.Instance,
                WriteIndented = true
            };

            return JsonSerializer.Serialize(this, option);
        }

        public void LoadFromJson(string json)
        {
            GameUser deserialized = JsonSerializer.Deserialize<GameUser>(json);
            this.chatId = deserialized.chatId;
            this.nickName = deserialized.nickName;
            this.bombs = deserialized.bombs;
            this.gameboardSide = deserialized.gameboardSide;
        }
    }
}