using System.Text.Json;

namespace NavalBattle
{
    public class User : IJsonConvertible
    {
        private string nickName;

        private Gameboard gameboard;

        /// <summary>
        /// verifico que el nombre no sea nulo o vacio
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
        /// <summary>
        /// es el constructor de la clase User, se le pasa el nombre del usuario y el tamanio del tablero
        /// </summary>
        /// <param name="nickName"></param>
        /// <param name="gameboardSize"></param>
        public User(string nickName, int gameboardSize)
        {
            this.NickName = nickName;
            Gameboard gameboard = new Gameboard(gameboardSize);
        }
        /// <summary>
        /// seguramente sea para colocar a un user en una sala de espera segun las preferencias del usuario
        /// </summary>
        /// <param name="user"></param>
        /// <param name="user1"></param>
        public void MatchMaking(User user,User user1) //hay que definir un tamaño desde el handler
        {
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