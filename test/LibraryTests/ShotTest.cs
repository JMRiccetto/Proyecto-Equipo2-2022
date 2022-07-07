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
            gameboard = new Gameboard(6);        }

        [Test]
        public void shotIs0()
        {
            Assert.AreEqual(gameboard.Xcount, 0);
        }


        [Test]
        public void AttackGameboardPrinter()
        {
            NavalBattle.Coords coords = new Coords("00");
            gameboard.AddShip(3, "00", "S");
            gameboard.RecieveAttack(coords);
            Assert.AreEqual(gameboard.Tcount, 1);
        }
    }
}