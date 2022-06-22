using System;
using System.Collections.Generic;
using System.IO;


namespace NavalBattle
{
    public class Gameboard : IGameboardContent
    {   
        private int side;

        public int Side
        {
            get
            {
                return this.side;
            }

            set
            {
                side = value;
            }
        }

        private List<Ship> ships = new List<Ship>();

        public List<Ship> Ships
        {
            get
            {
                return this.ships;
            }
        }
        private List<Bomb> bombs = new List<Bomb>();

        public List<Bomb> Bombs
        {
            get
            {
                return this.bombs;
            }
        }

        private List<Coords> water = new List<Coords>();

        public List<Coords> Water
        {
            get
            {
                return this.water;
            }
        }
        private bool bombSwitch;

        public bool BombSwitch
        {
            get
            {
                return bombSwitch;
            }
            set
            {
                bombSwitch = value;
            }
        }
        
        private bool doubleAttackSwitch;

        public bool DoubleAttackSwitch
        {
            get
            {
                return doubleAttackSwitch;
            }
            set
            {
                doubleAttackSwitch = value;
            }
        }

        public Gameboard (int side)
        {
            this.side = side;

            //this.gameboard = new string[side,side];
        }   

        /// <summary>
        /// Devuelve true si la coordenada se encuentra en el tablero.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool isValidCoord(Coords coord)
        {
            int coordX = (int)Char.GetNumericValue(coord.CoordsLocation[0]);
            int coordY = (int)Char.GetNumericValue(coord.CoordsLocation[1]);

            return (coordX < this.side && coordY < this.side);
        }

        /// <summary>
        /// Metodo que añade barcos al tablero. Los Ship se crean en Gameboard porque los contiene (Creator).
        /// </summary>
        /// <param name="length"></param>
        /// <param name="initialCoord"></param>
        /// <param name="direction"></param>
        public void AddShip(int length, string initialCoordStr, string direction)
        {
            Coords initialCoord = new Coords(initialCoordStr);

            if (!isValidCoord(initialCoord))
            {
                throw new InvalidCoordException("Coordenada no valida.");
            }

            direction.ToUpper();

            if(direction != "N" && direction != "S" && direction != "E" && direction != "W")
            {
                throw new Exception("Direccion no valida.");
            }

            Ship ship = new Ship(length, direction);
        
            int initialCoordX = (int)Char.GetNumericValue(initialCoordStr[0]);
            int initialCoordY = (int)Char.GetNumericValue(initialCoordStr[1]);

            //El Ship es una lista de coordenadas, donde el usuario ingresa la coordenada inicial, largo y direccion del barco. 
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
                throw new Exception("Barco fuera de rango.");
            }

            //Si el barco fue creado con exito, se evalúa que no comparta ninguna de sus coordenadas con otro barco.
            int validShipCounter = 0;

            foreach (Ship placedShip in ships)
            {
                foreach (Coords coord in ship.Coords)
                {
                    if (placedShip.ShipContainCoord(coord))
                    {
                        validShipCounter += 1;
                    }                     
                }                    
            }
            
            //Si el barco a agregar no comparte ninguna coordenada con ningun barco posicionado, se agrega al tablero.
            if (validShipCounter == 0)
            {
                ships.Add(ship);

                //Si ya fueron posicionados los tres barcos, el resto de las coordenadas se agregan a water.
                if (this.ships.Count == 3)
                {
                    AddWater();
                }
            }
            else
            {
                throw new Exception("Barcos superpuestos.");
            }             
        }
        
        /// <summary>
        /// Añade las coordenadas donde no se posicionaron barcos a water.
        /// </summary>
        private void AddWater()
        {
            int shipCoordChecker = 0;

            for(int i = 0; i < this.side; i++)
            {
                for(int j = 0; j < this.side; j++)
                {
                    Coords coord = new Coords(i.ToString()+j.ToString());

                    foreach(Ship ship in this.ships)
                    {        
                        if (ship.ShipContainCoord(coord))
                        {
                            shipCoordChecker++;
                        }
                    }

                    if (shipCoordChecker == 0)
                    {
                        this.water.Add(coord);
                    }

                    shipCoordChecker = 0;
                }
            }
        }
        /// <summary>
        /// Metodo que añade bombas al tablero.
        /// Se Crean y añaden en Gameboard por creator.
        /// Precondiciones: 
        ///     Solo se añaden tres bombas cualquiera sea el tamaño del tablero.
        ///     No puede haber dos bombas a menos de dos "casilleros" de distancia.
        /// </summary>
        public void AddBombs()
        {   
            //if (bombsSwitch)
            //{
                Random rnd = new Random();
    
                int i = 0;

                while(i < 3)
                {
                    int bombCoordX = rnd.Next(0, this.side -1);

                    int bombCoordY = rnd.Next(0, this.side - 1);

                    string bombCoordStr = bombCoordX.ToString() + bombCoordY.ToString();

                    int nearBombChecker = 0;

                    foreach (Bomb bomb in this.bombs)
                    {
                        if ((bomb.Coord.CoordsLocation == (bombCoordX+1).ToString() + (bombCoordY-1).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX+1).ToString() + (bombCoordY+1).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX+1).ToString() + (bombCoordY).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX-1).ToString() + (bombCoordY+1).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX-1).ToString() + (bombCoordY-1).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX-1).ToString() + (bombCoordY).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX).ToString() + (bombCoordY+1).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else if ((bomb.Coord.CoordsLocation == (bombCoordX).ToString() + (bombCoordY-1).ToString()))
                        {
                            nearBombChecker++;
                        }
                        else
                        {}
                    }
                        
                    if (nearBombChecker == 0)
                    {
                        Coords coord = new Coords(bombCoordStr);

                        Bomb bombToAdd = new Bomb(coord);

                        bombs.Add(bombToAdd);

                        i++;
                    }
                }
            //}
        }

        /// <summary>
        /// Metodo de la interfaz IGameboardContent que implementa Gameboard.
        /// Se aplica DIP para para imprimir los tableros de diferentes maneras dependiendo 
        /// de que jugador es, y quien lo quiere visualizar.
        /// </summary>
        /// <returns></returns>
        public string[,] GetGameboardToPrint()
        {   
            string[,] res = new string[this.side,this.side];

            foreach(Ship ship in this.ships)
            {
                foreach(Coords shipCoord in ship.Coords)
                {
                    int coordX = (int)Char.GetNumericValue(shipCoord.CoordsLocation[0]);
            
                    int coordY = (int)Char.GetNumericValue(shipCoord.CoordsLocation[1]);

                    if (shipCoord.HasBeenAttacked == true)
                    {
                        res[coordX,coordY] = "t";
                    }
                    else
                    {
                        res[coordX,coordY] = "o";
                    }
                }
            }
            
            foreach(Coords waterCoord in this.water)
            {
                int coordX = (int)Char.GetNumericValue(waterCoord.CoordsLocation[0]);
            
                int coordY = (int)Char.GetNumericValue(waterCoord.CoordsLocation[1]);

                if (waterCoord.HasBeenAttacked == true)
                {
                    res[coordX,coordY] = "x";
                }
            }
            return res;
        }

        /// <summary>
        /// Metodo donde se ataca a una coordenada del tablero.
        /// Devuelve el resultado del ataque.
        /// Se implementa en Gameboard ya que es la clase experta que contiene los datos.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public string RecieveAttack(Coords coord)
        {   
            if (this.ships.Count != 3)
            {
                throw new Exception("No estan todos los barcos posicionados.");
            }
            
            if(!isValidCoord(coord))
            {
                throw new InvalidCoordException("Coordenada no valida");
            }
 
            string res = "Agua";

            foreach (Ship placedShip in ships)
            {
                if(placedShip.ShipContainCoord(coord))
                {
                    placedShip.RecieveDamage(coord);

                    if (placedShip.IsSunk())
                    {
                    res = "Hundido";
                    }
                    else
                    {
                    res = "Tocado";
                    } 
                }
            }  

            foreach (Coords waterCoord in this.water)
            {
                if (waterCoord.CoordsEquals(coord))
                {
                    waterCoord.ChangeCoordState();
                }
            }

            foreach(Bomb bomb in bombs)
            {
                if (coord.CoordsEquals(bomb.Coord))
                {
                   Kaboom(coord); 
                   res = "Bomba";
                }
            }
            return res;
        }

        /// <summary>
        /// Metodo que se ejecuta cuando el tablero recibe un ataque en la coordenada donde se encuentra una bomba.
        /// Ataca a todas las coordenadas adyacentes a ella.
        /// Los condicionales del metodo checkean que no se ataque a una coordenada que no existe
        /// en caso que la bomba se encuentre en una posicion al "borde" del tablero.
        /// </summary>
        /// <param name="coord"></param>
        public void Kaboom(Coords coord)
        {
            int attackCoordX = (int)Char.GetNumericValue(coord.CoordsLocation[0]);
            
            int attackCoordY = (int)Char.GetNumericValue(coord.CoordsLocation[1]);

            if (attackCoordX+1 < this.side)
            RecieveAttack(new Coords((attackCoordX+1).ToString() + (attackCoordY).ToString()));

            if (attackCoordY+1 < this.side)
            RecieveAttack(new Coords((attackCoordX).ToString() + (attackCoordY+1).ToString()));

            if (attackCoordX+1 < this.side && attackCoordY-1 >= 0)
            RecieveAttack(new Coords((attackCoordX+1).ToString() + (attackCoordY-1).ToString()));

            if (attackCoordX+1 < this.side && attackCoordY+1 < this.side)
            RecieveAttack(new Coords((attackCoordX+1).ToString() + (attackCoordY+1).ToString()));

            if (attackCoordX-1 >= 0)
            RecieveAttack(new Coords((attackCoordX-1).ToString() + (attackCoordY).ToString()));

            if (attackCoordY-1 >= 0)
            RecieveAttack(new Coords((attackCoordX).ToString() + (attackCoordY-1).ToString()));

            if (attackCoordX-1 >=0 && attackCoordY-1 >=0)
            RecieveAttack(new Coords((attackCoordX-1).ToString() + (attackCoordY-1).ToString()));

            if (attackCoordX-1 >=0 && attackCoordY+1 < this.side)
            RecieveAttack(new Coords((attackCoordX-1).ToString() + (attackCoordY+1).ToString()));
        }

        /// <summary>
        /// Devuelve true si todos los barcos del tablero fueron hundidos.
        /// </summary>
        /// <returns></returns>
        public bool IsMatchFinished()
        {
            int finishMatchChecker = 0;

            foreach (Ship placedShip in ships)
            {
                if (placedShip.IsSunk())
                {
                    finishMatchChecker++;
                }
            }
            return (finishMatchChecker == ships.Count);
        }
    }     
}
