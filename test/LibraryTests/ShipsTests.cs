using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    public class ShipsTests
    {
        User user;
        Gameboard gameboard;
        Ship ship = new Ship(5, "Sur");
        Coords coords = new Coords("A1");

        [SetUp]
        public void Setup()
        {
            ship.AddShipCoord(coords);
        }

        [Test]
        public void GetCoordsShipTest()
        {
            Assert.AreEqual("00", ship.Coords);
        }

        [Test]
        public void ShipsIsSunkTest()
        {
            Coords coords2 = new Coords("B2");
            Assert.AreEqual(0, ship.IsSunk());
        }

        [Test]
        public void ShipsIsSunkTest2()
        {
            Coords coords2 = new Coords("A1");
            Assert.AreEqual(1, ship.IsSunk());
        }

        [Test]
        public void  ShipContainCoordTest()
        {
            Coords coords2 = new Coords("A1");
            Assert.AreEqual(true, ship.ShipContainCoord(coords2));
            
        }

        [Test]
        public void  ShipContainCoordTest2()
        {
            Coords coords2 = new Coords("B4");
            Assert.AreEqual(false, ship.ShipContainCoord(coords2)); 
        }


/// <summary>
/// estos test creo que deberia ir en coords pero no lo tengo claaro
/// </summary>

        [Test]
        public void RecieveDamageTest()
        {
            Coords coords2 = new Coords("A1");
            ship.RecieveDamage(coords2);
            
            Assert.AreEqual(true, coords2.HasBeenAttacked);

        }

        [Test]
        public void RecieveDamageTest2()
        {
            Coords coords2 = new Coords("A2");
            ship.RecieveDamage(coords2);
            Assert.AreEqual(null, coords2.HasBeenAttacked); //???????

        }
    }
}