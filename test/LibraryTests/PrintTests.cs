/* 

No pudimos testear los printer ya que es un void


using NUnit.Framework;
using NavalBattle;
using System.Text;
using System;

namespace Test.Library
{
    /// <summary>
    /// esta clase prueba que las clases que implementen las interface de IPrinter (y como arraste IGameboardContent)
    /// </summary>
    public class PrintTests
    {
        [SetUp]
        public void Setup()
        {
        }


        [Test]
        public void DefenseGameboardPrinter()
        {
            IPrinter printer = new DefenseGameboardPrinter();
            
            Gameboard gameboard = new Gameboard(4);

            gameboard.AddShip(2, "00", "S");

            gameboard.AddShip(2, "01", "S");

            gameboard.AddShip(2, "02", "S");

            Coords coord = new Coords("01");

            gameboard.RecieveAttack(coord);

            StringBuilder s = new StringBuilder();

            s.Append("  0  1  2  3");

            s.Append("\n");

            s.Append("0|o||x||o||~|");

            s.Append("\n");

            s.Append("1|o||o||o||~|");

            s.Append("\n");

            s.Append("2|~||~||~||~|");

            s.Append("\n");

            s.Append("3|~||~||~||~|");

            s.Append("\n");

            Assert.AreEqual(Console.WriteLine(s), printer.PrintGameboard(gameboard))
        }

        /// <summary>
        /// este test deberia probar que se imprima el AttackGameboard
        /// </summary>
        [Test]
        public void AttackGameboardPrinter()
        {
            
        }
    }
} */