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

        /// <summary>
        /// Este atributo es el largo de los barcos que se van posicionar. El primer barco es de largo 2, 
        /// el segundo de largo 3 y el tercero de largo 4.
        /// </summary>
        private int counterShipLength = 2;

        private long chatIdPlayer;

        public Player(int gameboardSide, long id)
        {
            this.gameboard = new Gameboard(gameboardSide);
            this.chatIdPlayer = id;
        }
        
        public long ChatIdPlayer
        {
            get
            {
                return this.chatIdPlayer;
            }
        }
        
        public int CounterShipLength
        {
            get
            {
                return this.counterShipLength;
            }
        }

        public Gameboard Gameboard
        {
            get
            {
                return this.gameboard;
            }   
        }

        public bool Turn
        {
            get
            {
                return turn;
            }
        }

        /// <summary>
        /// Metodo para posicionar barco en tablero propio.
        /// </summary>
        /// <param name="initialCoord"></param>
        /// <param name="direction"></param>
        public void PlaceShip (string initialCoord, string direction)
        {
            gameboard.AddShip(this.counterShipLength, initialCoord, direction);

            this.counterShipLength++;
        }

        /// <summary>
        /// Metodo para atacar tablero de otro jugador.
        /// </summary>
        /// <param name="coordStr"></param>
        /// <param name="gameboard"></param>
        /// <returns></returns>
        public string Attack(string coordStr, Gameboard gameboard)
        {
            Coords coord = new Coords(coordStr);

            string res = gameboard.RecieveAttack(coord);

            return res;
        }


        /// <summary>
        /// Cambia el turno del jugador.
        /// </summary>
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