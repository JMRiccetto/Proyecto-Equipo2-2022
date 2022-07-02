using System;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;

namespace NavalBattle
{
    /// <summary>
    /// Player es una intancia de los usuarios cuando se crea un Match.
    /// </summary>
    public class Player   
    {  
        private Gameboard gameboard;     

        private bool turn = false; 

        private int counterShipLength = 2;

        private long chatIdPlayer;

        public long ChatIdPlayer
        {
            get
            {
                return this.chatIdPlayer;
            }
        }
        public Player(int gameboardSide, long id)
        {
            this.gameboard = new Gameboard(gameboardSide);
            this.chatIdPlayer = id;
        }

        public Gameboard Gameboard
        {
            get
            {
                return this.gameboard;
            }   
        }
        
        public void PlaceShip (string initialCoord, string direction)
        {

            gameboard.AddShip(this.counterShipLength, initialCoord, direction);

            this.counterShipLength++;
        }

        public string Attack(string coordStr, Gameboard gameboard)
        {
            Coords coord = new Coords(coordStr);

            string res = gameboard.RecieveAttack(coord);

            return res;
        }

        public bool Turn
        {
            get
            {
                return turn;
            }
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