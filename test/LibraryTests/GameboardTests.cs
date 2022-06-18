using System;
using NUnit.Framework;
using NavalBattle;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class GameboardTest
    {
        Gameboard gameboard;
        Ship ship;
        Bomb bomb;
        List<Ship> ships;
        List<Bomb> bombs;
        int side = 7;
        int length = 2;
        string initialCoord = "a4";
        string direction = "S";
        string coord = "b6";

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
            this.gameboard.AddBombs();
            Assert.IsNotEmpty(this.bombs);
        }
    }
}