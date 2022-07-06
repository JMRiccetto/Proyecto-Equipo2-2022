using System.Text.Json;
using System.Text.Json.Serialization;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace NavalBattle
{
    /// <summary>
    /// Clase usuario, posee atributos que luego serán utilizados por Player.
    /// </summary>
    public class GameUser
    {   
        private long chatId;

        private bool bombs = false;

        private int gameboardSide = 6;

        private string nickName;

        private Player player;

        private UserState state = UserState.NotInGame;

        /// <summary>
        /// Constructor de GameUser.
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="aChatId"></param>
        public GameUser(string nickName, long aChatId)
        {
            this.chatId = aChatId;
            this.nickName = nickName;
        }

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

        /// <summary>
        /// Gets y Sets de los Usuarios.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Gets y Sets del interruptor de bombas.
        /// </summary>
        /// <value></value>
        public bool Bombs
        {
            get
            {
                return this.bombs;
            }

            set
            {
                this.bombs = value;
            }
        }

        /// <summary>
        /// Gets y Sets del lado de Gameboard que se creará en player.
        /// </summary>
        /// <value></value>
        public int GameboardSide
        {
            get
            {
                return this.gameboardSide;
            }

            set
            {
                this.gameboardSide = value;
            }
        }

        /// <summary>
        /// Gets y Sets del nombre de usuario.
        /// </summary>
        /// <value></value>
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

        /// <summary>
        /// Gets y Sets de Player.
        /// </summary>
        /// <value></value>
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
    }
}