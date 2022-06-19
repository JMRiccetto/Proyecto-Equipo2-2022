namespace NavalBattle
{ 
    /// <summary>
    /// User contiene el nombre del usuario y su tablero (lo crea)
    /// </summary>
    public class User
    {
        //se crea la clase User, se agrega el atributo de NickName y el metodo MatchMaking()
        private string nickName;
        private Gameboard gameboard { get; set; }

        /// <summary>
        /// verifico que el nombre no sea nulo o vacio
        /// </summary>
        /// <value></value>
        public string NickName
        {
            get
            {
                return this.NickName;
            }
            private set
            {
                this.NickName = NickName != null ? NickName : value;
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
    }
}