using System;

namespace NavalBattle
{
    public class Bomb
    {
        private string coord;

        public string Coords
        {
            get
            {
                return this.coord;
            }
        }

        public Bomb (string coord)
        {
            this.coord = coord;
        }
    }
}