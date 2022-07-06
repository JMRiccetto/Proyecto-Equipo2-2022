using System;

namespace NavalBattle
{
    /// <summary>
    /// Clase que representa a las bombas que irán dentro del tablero.
    /// </summary>
    public class Bomb
    {
        private Coords coord;

        /// <summary>
        /// La coordenada de la bomba se crea en Bomb por creator.
        /// </summary>
        /// <param name="stringCoord"></param>
        public Bomb(string stringCoord)
        {
            Coords coord = new Coords(stringCoord);
            this.coord = coord;
        }

        /// <summary>
        /// Gets de las coordenadas de la bomba.
        /// </summary>
        /// <value></value>
        public Coords Coord
        {
            get
            {
                return this.coord;
            }
        }
    }
}