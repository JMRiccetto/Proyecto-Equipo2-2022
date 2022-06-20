using System.Text.Json;

namespace NavalBattle
{
    public class User : IJsonConvertible
    {
        private string nickName;

        private Gameboard gameboard;

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

        public User(string nickName)
        {
            this.NickName = nickName;
        }

        public void MatchMaking() //hay que ver exactamente que hace esta funcion
        {
            //generar codigo para que el otro usuario se conecte a una partida
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