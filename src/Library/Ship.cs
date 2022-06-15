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

        //Añade una coordenada al barco.
        public void AddShipCoord(Coords coord)
        {
            //Coords coord = new Coords(coordString);
            this.coords.Add(coord);
        }

        //Metodo que devuelve si el barco esta hundido o no.
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

            return (sunkChecker == 0);
        }
    }
}