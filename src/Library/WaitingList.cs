using System;
using System.Collections.Generic;
using System.IO;

namespace NavalBattle
{
    public class WaitingList
    {
        private List<Player> players;

        public WaitingList()
        {
            this.players = new List<Player>();
        }

        public List<Player> Players
        {
            get
            {
                return this.players;
            }

            set
            {
                this.players = Players;
            }
        }
    }
}