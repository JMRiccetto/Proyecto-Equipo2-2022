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
                
            Players[0].Turn = true;

            Players[1].Turn = false;

            user1.Player = this.Players[0];
            user2.Player = this.Players[1];
        }

        public void Attack(Coords coord, Gameboard gameboard)
        {
            gameboard.RecieveAttack(coord);
        }
    }
}

