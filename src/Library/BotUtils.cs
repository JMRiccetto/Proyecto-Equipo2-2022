using System.Collections.Generic;

namespace NavalBattle
{
    public static class BotUtils
    {
        private static List<Match> matchList = new List<Match>();

        private static List<Match> inGameMatchList = new List<Match>();

        private static void StartMatch(Match match)
        {
            matchList.Add(match);
        }

        public static void MatchPlayers(GameUser user1, GameUser user2)
        {
            Match match = new Match();
            match.AddPlayers(user1, user2);
            StartMatch(match);
        }

        public static List<Match> MatchList
        {
            get
            {
                return matchList;
            }
            set
            {
                matchList = value;
            }
        }

        public static List<Match> InGameMatchList
        {
            get
            {
                return inGameMatchList;
            }

            set
            {
                inGameMatchList = value;
            }
        }

        public static void Surrender(GameUser user)
        {
            foreach (Ship ship in user.Gameboard.Ships)
            {
                //ship.IsSunk = true;
            }
        }

        //Una vez que todos los barcos de un jugador han sido hundidos, finaliza el juego.
        /* public static bool EndGame(Player[] players)
        {
            foreach (Player player in players)
            {
                foreach (Ship ship in player.Gameboard.Ships)
                {
                    if (ship.ShipState == Ship.State.Sunk)
                    {
                        return true;
                    }
                    return false;
                }
                return false;
            }
            return false;
        } */
    }
}