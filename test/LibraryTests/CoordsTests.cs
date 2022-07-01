using System;
using NUnit.Framework;
using NavalBattle;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class CoordsTest
    {
    
        [SetUp]
        public void Setup()
        {
        }

        //Testea que se cambia el estado de una coordenada a atacada.
        [Test]
        public void AttackedCoordTest()
        {
            Coords coord = new Coords("56");        
            coord.ChangeCoordState();
            Assert.IsTrue(coord.HasBeenAttacked);
        }

        //Testea que el estado por defecto de una coordenada es no atacada.
        [Test]
        public void NotAttackedCoordTest()
        {
            Coords coord = new Coords("56");
            Assert.IsFalse(coord.HasBeenAttacked);
        }

        //Testea que dos coordenadas sean iguales.
        [Test]
        public void CoordsEqualsTest()
        {
            Coords coord1 = new Coords("56");

            Coords coord2 = new Coords("56");

            Assert.IsTrue(coord1.CoordsEquals(coord2));
        }

        //Testea que dos coordenadas sean distintas.
        [Test]
        public void CoordsNotEqualsTest()
        {
            Coords coord1 = new Coords("56");

            Coords coord2 = new Coords("57");

            Assert.IsFalse(coord1.CoordsEquals(coord2));
        }
    }
}