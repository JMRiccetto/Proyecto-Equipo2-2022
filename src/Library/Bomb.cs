using System;

namespace NavalBattle
{
    public class Bomb
    {
        private Coords coord;

        public Coords Coord
        {
            get
            {
                return this.coord;
            }
        }

        /// <summary>
        /// La coordenada de la bomba se crea en Bomb por creator.
        /// </summary>
        /// <param name="stringCoord"></param>
        public Bomb (string stringCoord)
        {
            Coords coord = new Coords("stringCoord");
            this.coord = coord;
        }
    }
}