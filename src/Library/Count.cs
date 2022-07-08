using System;
using System.Text;

namespace NavalBattle
{

    public class Count
    {
        public int shipCounter = 0;
        public int waterCounter = 0;
        public int bombCounter = 0;


        public Count()
        {
            this.shipCounter = shipCounter;
            this.waterCounter = waterCounter;
            this.bombCounter = bombCounter;
        }

        public int AddShipCounter()
        {
            this.shipCounter += 1;
            return this.shipCounter;
        }

        public int AddWaterCounter()
        {
            this.waterCounter += 1;
            return this.waterCounter;
        }

        public int AddBombCounter()
        {
            this.bombCounter += 1;
            return this.bombCounter;
        }
    }
}