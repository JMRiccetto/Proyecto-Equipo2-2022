using NUnit.Framework;
using NavalBattle;
using System.Text;
using System;

namespace Test.Library
{
    public class ShotTests
    {
        Gameboard gameboard;
        Player player;
        GameUser user;

        [SetUp]
        public void Setup()
        {
            gameboard = new Gameboard(6);        
        }

        [Test]
        public void shotWaterIs0()
        {
            Assert.AreEqual(gameboard.Xcount, 0);
        }


        [Test]
        public void shotTakeIs0()
        {
            NavalBattle.Coords coords = new Coords("11");
            gameboard.AddShip(3, "00", "S");
            gameboard.RecieveAttack(coords);
            Assert.AreEqual(gameboard.Tcount, 0);
        }

        [Test]
        public void shotWaterIs01()
        {
            NavalBattle.Coords coords = new Coords("44");
            gameboard.AddShip(2, "11", "S");
            gameboard.RecieveAttack(coords);
            Assert.AreEqual(gameboard.Xcount, 1);
        }

        [Test]
        public void shotTakeIs1()
        {
            NavalBattle.Coords coords = new Coords("00");
            gameboard.AddShip(3, "00", "S");
            gameboard.RecieveAttack(coords);
            Assert.AreEqual(gameboard.Tcount, 1);
        }
    }
}