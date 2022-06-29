namespace NavalBattle
{
    public class Match
    { 
        private Player player1;

        private Player player2;


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
            this.player1 = new Player(user1.GameboardSide);
            this.player2 = new Player(user2.GameboardSide);
                
            user1.Player = player1;
            user2.Player = player2;

            player1.Turn = true;

            player2.Turn = false;
        }

        public void Attack(Coords coord, Gameboard gameboard)
        {
            gameboard.RecieveAttack(coord);
        }

        
    }
}

