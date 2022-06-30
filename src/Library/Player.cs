using System;

namespace NavalBattle
{
    /// <summary>
    /// Player es una intancia de los usuarios cuando se crea un Match.
    /// </summary>
    public class Player   
    {  
        private Gameboard gameboard;

        private bool turn;

        public Player(int gameboardSide)
        {
            this.gameboard = new Gameboard(gameboardSide);
        }

        public bool Turn
        {
            get
            {
                return turn;
            }
            set
            {
                this.turn = value;
            }
        }

        public Gameboard Gameboard
        {
            get
            {
                return this.gameboard;
            }   
        }

        public void PlaceShip(int length, string initialCoord, string direction)
        {
            gameboard.AddShip(length, initialCoord, direction);
        }

        public void ChangeTurn()
        {
            if(this.turn == true)
            {
                this.turn = false;
            }
            else
            {
                this.turn = true;
            }
        }
    }
}