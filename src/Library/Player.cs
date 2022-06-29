using System;

namespace NavalBattle
{
    /// <summary>
    /// Player es una intancia de los usuarios cuando se crea un Match.
    /// </summary>
    public class Player   
    {  
        private Gameboard gameboard;

        public Player(int gameboardSide)
        {
            this.gameboard = new Gameboard(gameboardSide);
        }

        public Gameboard Gameboard
        {
            get
            {
                return this.gameboard;
            }   
        }

        public void Attack(Coords coord, Gameboard gameboard)
        {
            gameboard.RecieveAttack(coord);
        }

        public void PlaceShip(int length, string initialCoord, string direction)
        {
            this.gameboard.AddShip(length, initialCoord, direction);
        }
    }
}