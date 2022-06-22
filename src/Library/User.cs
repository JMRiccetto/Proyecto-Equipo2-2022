using System.Text.Json;

namespace NavalBattle
{
    public class GameUser : IJsonConvertible
    {
        private bool bombs;

        public bool Bombs
        {
            get
            {
                return bombs;
            }
        }

        private bool doubleAttack;

        public bool DoubleAttack
        {
            get
            {
                return doubleAttack;
            }
        }
        private int gameboardSide;

        public int GameboardSide
        {
            get
            {
                return gameboardSide;
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

        private Gameboard gameboard;

        public Gameboard Gameboard
        {
            get
            {
                return this.gameboard;
            }
            set
            {
                this.gameboard = Gameboard;
            }
        }

        public Player player {get; set;}


        public GameUser(string nickName)
        {
            this.NickName = nickName;
        }
        
        /// <summary>
        /// El usuuario busca partida eligiendo las caracteristicas con las que quiere jugar.
        /// </summary>
        /// <param name="gameboardSide"></param>
        /// <param name="bombs"></param>
        /// <param name="doubleAttack"></param>
        public void SearchGame(int gameboardSide, bool bombs, bool doubleAttack) 
        {
            this.gameboardSide = gameboardSide;

            this.bombs = bombs;

            this.doubleAttack = doubleAttack;

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