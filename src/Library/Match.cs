namespace NavalBattle
{
    public class Match
    { 
        public readonly Player[] Players = new Player[2];

        private int id;

        public int Id
        {
            get
            {
                return id;
            }
        }
        
        public Match(GameUser user1, GameUser user2)
        { 
            this.Players[0] = new Player(user1.GameboardSide);
            this.Players[1] = new Player(user2.GameboardSide);

            this.Players[0].ChatId = user1.ChatId;
            this.Players[1].ChatId = user2.ChatId;
        }
    }
}

