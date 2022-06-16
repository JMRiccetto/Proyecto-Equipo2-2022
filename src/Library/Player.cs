namespace NavalBattle
{
    public class Player   //Esta clase va a manejar el usuario y el tablero del jugador
    {
        //se crea la clase player  y se agregan lo atributos de User y GameBoard
        private User user;
        private Gameboard gameboard;
        private bool turn;
        public Player(User user, int gameboardSize)
        {
            this.user = user;
            Gameboard gameboard = new Gameboard(gameboardSize);
            this.turn = false; 
        }

        public bool Turn
        {
            get
            {
                return this.turn;
            }
            set
            {
                this.turn = Turn;
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
    }
}