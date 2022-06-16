namespace NavalBattle
{
    public class User   //Esta clase va a manejar un nickname del usuario y la funcionalidad de MatchMaking
    {
        //se crea la clase User, se agrega el atributo de NickName y el metodo MatchMaking()

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

        public User(string nickName)
        {
            this.NickName = nickName;
        }

        public void MatchMaking() //hay que ver exactamente que hace esta funcion
        {
            //generar codigo para que el otro usuario se conecte a una partida
        }
    }
}