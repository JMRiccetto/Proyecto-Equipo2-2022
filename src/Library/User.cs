namespace NavalBattle
{ 
    public class User   //Esta clase va a manejar un nickname del usuario y la funcionalidad de MatchMaking
    {
        //se crea la clase User, se agrega el atributo de NickName y el metodo MatchMaking()
        private string nickName;
        private Gameboard gameboard { get; set; }

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

        public User(string nickName, int gameboardSize)
        {
            this.NickName = nickName;
            Gameboard gameboard = new Gameboard(gameboardSize);
        }

        public void MatchMaking(User user,User user1) //hay que definir un tamaño desde el handler
        {
        }
    }
}