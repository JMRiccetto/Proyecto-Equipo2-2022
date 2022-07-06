using System;
using System.Collections.Generic;
using System.IO;

namespace NavalBattle
{
    /// <summary>
    /// Clase que representa a los barcos que van dentro del tablero.
    /// </summary>
    public class Ship
    {
        private string direction;

        private int length;

        private bool sunk;

        private List<Coords> coords;

        /// <summary>
        /// Constructor del barco.
        /// </summary>
        /// <param name="length"></param>
        /// <param name="direction"></param>
        public Ship(int length, string direction)
        {
            this.length = length;
            this.direction = direction;
            this.sunk = false;
            this.coords = new List<Coords>();
        }

        public List<Coords> Coords 
        {
            get
            {
                return coords;
            }
        }

        /// <summary>
        /// Gets del largo del barco.
        /// </summary>
        /// <value></value>
        public int Length
        {
            get
            {
                return this.length;
            }
        }

        /// <summary>
        /// Añade una coordenada al barco.
        /// La coordenada a agregar se crea en Ship por creator.
        /// </summary>
        /// <param name="stringCoord"></param>
        public void AddShipCoord(string stringCoord)
        {
            Coords coord = new Coords(stringCoord);
            this.coords.Add(coord);
        }

        /// <summary>
        /// //Metodo que devuelve si el barco esta hundido o no.
        /// </summary>
        /// <returns></returns>
        public bool IsSunk()
        {
            int sunkChecker = 0;

            foreach (Coords coord in this.Coords)
            {
                if (!coord.HasBeenAttacked)
                {
                    sunkChecker += 1;
                }
            }
            return sunkChecker == 0;
        }

        /// <summary>
        /// Devuelve true si el barco contiene la coordenada pasada por parametro.
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool ShipContainCoord(Coords coord)
        {
            foreach(Coords shipCoord in coords)
            {
                if (shipCoord.CoordsEquals(coord))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Cambia el estado de la coordenada del barco que fue atacada.
        /// </summary>
        /// <param name="coord"></param>
        public void RecieveDamage(Coords coord)
        {
            foreach (Coords shipCoord in this.coords)
            {
                if (shipCoord.CoordsEquals(coord))
                {
                    shipCoord.ChangeCoordState();
                }
            }
        }
    }
}