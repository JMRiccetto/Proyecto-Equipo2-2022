using System;
using NUnit.Framework;
using NavalBattle;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GameboardTest
    {
        private Gameboard gameboard;
        private Ship ship;
        private Bomb bomb;
        private List<Ship> ships;
        private List<Bomb> bombs;
        private int side = 7;
        private int length = 2;
        private string initialCoord = "a4";
        private string direction = "S";
        private string coord = "b6";

        [SetUp]
        public void Setup()
        {
            //this.side = side;
            this.gameboard = new Gameboard(side);
            this.ship = new Ship(length, initialCoord);
            this.bomb = new Bomb(coord);
            this.ships = new List<Ship>();
            this.bombs = new List<Bomb>();
        }

        [Test]
        public void ShipsTest()
        {
            Assert.IsEmpty(this.ships);
        }

        [Test]
        public void addShipsTest()
        {
            this.gameboard.addShip(length, initialCoord, direction);
            Assert.IsNotEmpty(this.ships);
        }

        [Test]
        public void BombsTest()
        {
            Assert.IsEmpty(this.bombs);
        }

        [Test]
        public void addBombTest()
        {
            this.gameboard.AddBomb();
            Assert.IsNotEmpty(this.bombs);
        }
    }
}