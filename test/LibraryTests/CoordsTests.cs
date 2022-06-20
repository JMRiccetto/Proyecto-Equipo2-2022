using System;
using NUnit.Framework;
using NavalBattle;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class CoordsTest
    {
        Coords coord;
        Coords coord2;
        Coords coord3;

        string coordsLocation = "a6";
        string coordsLocation2 = "a6";
        string coordsLocation3 = "b2";

        bool hasBeenAttacked = false;


        [SetUp]
        public void Setup()
        {
            this.coord = new Coords(coordsLocation);
            this.coord2 = new Coords(coordsLocation2);
            this.coord3 = new Coords(coordsLocation3);

        }

        [Test]
        public void AttackedCoordTest()
        {
            this.coord.CoordsEquals(coord2);
            this.coord.ChangeCoordState();
            Assert.IsTrue(this.hasBeenAttacked);
        }

                [Test]
        public void NotAttackedCoordTest()
        {
            this.coord.CoordsEquals(coord3);
            this.coord.ChangeCoordState();
            Assert.IsFalse(this.hasBeenAttacked);
        }
    }
}