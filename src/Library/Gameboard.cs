using System;
using System.Collections.Generic;
using System.IO;


namespace NavalBattle
{
    public class Gameboard : IGameboardContent
    {   
        private int side;

        private string[,] gameboard;

        private List<Ship> ships;

        private List<Bomb> bombs;

        public Gameboard (int side)
        {
            this.side = side;

            this.gameboard = new string[side,side];

            this.ships = new List<Ship>();

            //if (Match.Bombs == true) 
            this.bombs = new List<Bomb>();
        }   

        //Metodo que añade barcos al tablero.
        //Los Ship se crean en Gameboard por creator.
        public void addShip(int length, string initialCoord, string direction)
        {
            Ship ship = new Ship(length, direction);
            
            int initialCoordX = (int)Char.GetNumericValue(initialCoord[0]);
            int initialCoordY = (int)Char.GetNumericValue(initialCoord[1]);

            bool validShip = true;

            //El Ship es una lista de coordenadas(string), donde el usuario ingresa la coordenada inicial, largo y direccion del barco. 
            //Con estos datos se checkea si es valida la posicion del barco en el tablero y se agregan al barco el resto de sus coordenadas. 
            if ((direction == "N") && (initialCoordX - length >= -1))
            {
                for (int i = 0; i < length; i++)
                {   
                    Coords coord = new Coords(initialCoordX.ToString() + initialCoordY.ToString());                                   
                    ship.AddShipCoord(coord);
                    initialCoordX--; 
                }
            }  
            else if ((direction == "E") && (initialCoordY + length <= this.side))
            {
                for (int i = 0; i < length; i++)
                {
                    Coords coord = new Coords(initialCoordX.ToString() + initialCoordY.ToString());                                   
                    ship.AddShipCoord(coord);
                    initialCoordY++;
                }
            }
            else if ((direction == "S") && (initialCoordX + length <= this.side))
            {
                for (int i = 0; i < length; i++)
                {
                    Coords coord = new Coords(initialCoordX.ToString() + initialCoordY.ToString());                                   
                    ship.AddShipCoord(coord);
                    initialCoordX++;
                }
            }
            else if ((direction == "W") && (initialCoordY - length >= -1))
            {
                for (int i = 0; i < length; i++)
                {
                    Coords coord = new Coords(initialCoordX.ToString() + initialCoordY.ToString());                                   
                    ship.AddShipCoord(coord);
                    initialCoordY--;
                }
            }
            else
            {  
                validShip = false;
                Console.WriteLine("Barco fuera de rango");
            }

            //Si el barco fue creado con exito, tambien tenemos que evaluar que no comparta ninguna de sus coordenadas con otro barco.
            if (validShip)
            {
                int validShipCounter = 0;

                foreach (Ship placedShip in ships)
                {
                    foreach (Coords placedCoord in placedShip.Coords)
                    {
                        foreach (Coords coord in ship.Coords)
                        {
                            if (coord.CoordsLocation == placedCoord.CoordsLocation)
                            {
                                validShipCounter += 1;
                            }                     
                        }                    
                    }
                }
                
                //Si el barco a agregar no comparte ninguna coordenada con ningun barco posicionado, se agrega al tablero.
                if (validShipCounter == 0)
                {
                    ships.Add(ship);

                    foreach (Coords coord in ship.Coords)
                    {
                        int shipCoordX = (int)Char.GetNumericValue(coord.CoordsLocation[0]);
                        int shipCoordY = (int)Char.GetNumericValue(coord.CoordsLocation[1]);

                        this.gameboard[shipCoordX, shipCoordY] = "o";
                    }
                    Console.WriteLine("Barco colocado correctamente");
                }
                else
                {
                    Console.WriteLine("Barcos superpuestos");
                }
            }     
        }
        
        //Las Bomb se crean en Gameboard por creator.
        public void AddBomb()
        {   
            Random rnd = new Random();
            
            int bombCoordX = rnd.Next(0, this.side -1);

            int bombCoordY = rnd.Next(0, this.side - 1);

            Bomb bomb = new Bomb(bombCoordX.ToString() + bombCoordY.ToString());

            bombs.Add(bomb);
        }

        //Devuelve el contenido del tablero que se va a imprimir.
        public string[,] GetGameboardToPrint()
        {      
            return this.gameboard;
        }

        //Metodo llamado desde la logica de la partida cuando un jugador ataca a otro.
        public void RecieveAttack(Coords coord)
        {   
            int woundedShipChecker = 0;

            foreach (Ship placedShip in ships)
            {
                foreach(Coords placedCoord in placedShip.Coords)
                {
                    if(placedCoord.CoordsLocation == coord.CoordsLocation) 
                    {
                        woundedShipChecker += 1;
                        
                        placedCoord.HasBeenAttacked = true;    
                    }
                }
            }
            
            int attackCoordX = (int)Char.GetNumericValue(coord.CoordsLocation[0]);
            int attackCoordY = (int)Char.GetNumericValue(coord.CoordsLocation[1]);
            
            if (woundedShipChecker == 1)
            {
                this.gameboard[attackCoordX, attackCoordY] = "t";       
            }
            else
            {
                this.gameboard[attackCoordX, attackCoordY] = "x";       
            }
        }      
    }     
}
