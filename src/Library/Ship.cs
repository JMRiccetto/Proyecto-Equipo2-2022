using System;
using System.Collections.Generic;
using System.IO;

namespace NavalBattle
{
    public class Ship
    {
        private string direction;

        private int length;

        private bool sunk;

        private List<Coords> coords;

        public List<Coords> Coords 
        {
            get
            {
                return coords;
            }
        }
        
        public Ship (int length, string direction)
        {
            this.length = length;
            this.direction = direction;
            this.sunk = false;
            this.coords = new List<Coords>();
        }

        /// <summary>
        /// //Añade una coordenada al barco.
        /// </summary>
        /// <param name="coord"></param>
        public void AddShipCoord(Coords coord)
        {
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
            foreach(Coords shipCoord in this.coords)
            {
                if (shipCoord.CoordsEquals(coord))
                {
                    shipCoord.ChangeCoordState();
                }
            }
        }
    }
}