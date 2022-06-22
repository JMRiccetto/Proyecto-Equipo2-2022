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
    }
}