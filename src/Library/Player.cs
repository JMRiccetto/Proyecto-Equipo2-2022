using System;
using System.Collections.Generic;
using System.IO;

namespace NavalBattle
{
    public class Player    //Esta clase va a manejar el usuario y el tablero del jugador (personalizado desde el menu)
    {
        //se crea la clase player  y se agregan lo atributos de User y GameBoard
        private User user;
        private Gameboard gameBoard;

        public Gameboard GameBoard
        {
            get { return gameBoard; }
            set { gameBoard = value; }
        }
        private bool Trun;          //esto declara el turno de cada jugador
        public Player(User user)
        {
            this.user = user;
            Gameboard gameBoard = new Gameboard(8); //se crea el tablero del jugador, este debe de cambiar luego por un valor personalizado del menu
        }

        public bool Turn 
        {
            get
            {
                return this.Turn;
            }
            set
            {
                this.Turn = Turn;
            }
        }
    }
}