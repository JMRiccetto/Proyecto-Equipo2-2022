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

        public Bomb (Coords coord)
        {
            this.coord = coord;
        }
    }
}