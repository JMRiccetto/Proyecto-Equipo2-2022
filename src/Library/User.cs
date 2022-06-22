using System.Text.Json;

namespace NavalBattle
{
    public class GameUser : IJsonConvertible
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

        public GameUser(string nickName)
        {
            this.NickName = nickName;
            this.gameboard = new Gameboard();
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