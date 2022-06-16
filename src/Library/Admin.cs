using System.Collections.Generic;

namespace NavalBattle
{
    public class Admin
    {
        List<Match> matchList = new List<Match>();

        public void UserRegister(string nickname)
        {
            
        }

        public void CreateMatch()
        {
            Match match = new Match();
            this.matchList.Add(match);
        }

        public void MatchPlayers(User user1, User user2, Match match)
        {
            match.AddPlayers(user1, user2);
        }


        public void AttackInfo()
        {
            
        }

        //Una vez que todos los barcos de un jugador han sido hundidos, finaliza el juego.
        public bool EndGame(Player[] players)
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
        }
    }
}