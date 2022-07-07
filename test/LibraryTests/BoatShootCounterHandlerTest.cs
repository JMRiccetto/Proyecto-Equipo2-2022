using NUnit.Framework;
using Telegram.Bot.Types;
using System.Text;
using NavalBattle;
using System;

namespace Test.Library
{
    // Test que demuestra que se pueden contar los disparos al agua de ambos jugadores.
    public class BoatShootsCounterHandlerTest
    {
        private BoatShootsCounterHandler firstHandler;

        private GameUser user1;

        private GameUser user2;

        private Match match;

        [SetUp]
        public void Setup()
        {
            this.firstHandler = new BoatShootsCounterHandler(null);

            this.user1 = new GameUser("Amelia", 1);
            this.user2 = new GameUser("Iris", 2);

            this.user1.GameboardSide = 7;
            this.user1.Bombs = false;

            this.user2.GameboardSide = 7;
            this.user2.Bombs = false;

            this.match = new Match(this.user1, this.user2);

            this.firstHandler.Match = this.match;

        }

        // Verifica que el contador está en cero al iniciar la partida.
        [Test]
        public void CounterAtZeroTest()
        {
            Assert.AreEqual(0, this.firstHandler.BoatShootsCounter);
        }

        // Test que verifica que el contador sube cuando ocurren ataques.
        // El 9 proviene del número de casillas que ocupan los barcos.
        [Test]
        public void BoatShootCounterIncreaseTest()
        {
            this.firstHandler.Match.Players[0].Gameboard.AddShip(3, "00", "S");

            this.firstHandler.Match.Players[0].Gameboard.AddShip(4, "34", "W");

            this.firstHandler.Match.Players[0].Gameboard.AddShip(2, "40", "E");

            this.firstHandler.Match.Players[0].Gameboard.AddWater();

            foreach (Ship ship in this.firstHandler.Match.Players[0].Gameboard.Ships)
            {
                foreach (Coords coord in ship.Coords)
                {
                    this.firstHandler.Match.Players[1].Attack(coord.CoordsLocation, this.firstHandler.Match.Players[0].Gameboard);
                    this.firstHandler.BoatShootsCounter++;
                }
            }
            Assert.AreEqual(9, this.firstHandler.BoatShootsCounter);
        }
    }
}