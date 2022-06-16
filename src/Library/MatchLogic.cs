using System.Collections.Generic;

namespace NavalBattle
{
    public class MatchLogic : Match
    {
        public void Attack(Coords coords)
        {
            if (players[0].Turn)
            {
                if (!coords.HasBeenAttacked)
                {
                    players[1].Gameboard.coords.HasBeenAttacked = true;
                    players[1].Gameboard.RecieveAttack(coords);
                }
                else
                {
                    coords.HasBeenAttacked = true;
                    players[0].Gameboard.RecieveAttack(coords);
                }
            }
        }
    
        public void ValidateCoord ()
        {

        }

        public void PlaceShip(string initialCoordsString, string direction)
        {
            Coords initialCoords = new Coords(initialCoordsString);
            for (int i = 2; i < 4; i++)
            {
                if (i <= 4 || players[0].Turn)
                {
                    players[0].Gameboard.addShip(i, initialCoords, direction);

                    players[0].Turn = false;
                    players[1].Turn = true;
                }
                else if (i <= 4 || players[1].Turn)
                {
                    players[1].Gameboard.addShip(i, initialCoords, direction);

                    players[1].Turn = false;
                    players[0].Turn = true;
                }
            }
        }

        public void Surrender(Player player)
        {
            foreach (Ship ship in player.Gameboard.Ships)
            {
                ship.ShipState = Ship.State.Sunk;
            }
        }
    }
}