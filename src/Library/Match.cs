namespace NavalBattle
{
    public class Match
    { 
        private Player player1;

        private Player player2;

        private bool turn = true;

        public bool Turn
        {
            get
            {
                return turn;
            }

            set
            {
                turn = value;
            }
        }
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
                
            user1.player = player1;
            user2.player = player2;
        }

        public void Attack(Coords coord)
        {
            if (turn == true)
            {
                player2.Gameboard.RecieveAttack(coord);
                turn = false;
            }
            else
            {
                player1.Gameboard.RecieveAttack(coord);
                turn = true;
            }
        }

        public void PlaceShip(int length, string initialCoord, string direction)
        {
            if (turn == true)
            {
                player1.Gameboard.AddShip(length, initialCoord, direction);
                turn = false;
            }
            else
            {
                player2.Gameboard.AddShip(length, initialCoord, direction);
                turn = true;
            }
        }
    }
}

