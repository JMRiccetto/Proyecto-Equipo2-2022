using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    /// <summary>
    /// clase de prueba para los Ships
    /// </summary>
    public class ShipsTests
    {
        Ship ship;
        Coords coords;


        /// <summary>
        /// se ingresa un ship, una coordenada y se agrega al barco
        /// </summary>
        [SetUp]
        public void Setup()
        {
            this.ship = new Ship(5, "Sur");
            this.coords = new Coords("A1");

            ship.AddShipCoord(coords);
        }
        /// <summary>
        /// este test prueba que la coordenada que se quiera colocar un barco sea la correcta (A1 = 00)
        /// </summary>
        [Test]
        public void GetCoordsShipTest()
        {
            Assert.AreEqual("00", ship.Coords);
        }
        /// <summary>
        /// est test prueba si la coordenada que se se ingreso contiene un barco hundido (debe dar negativo osea 0)
        /// </summary>
        [Test]
        public void ShipsIsSunkTest()
        {
            Coords coords2 = new Coords("B2");
            Assert.AreEqual(0, ship.IsSunk());
        }
        /// <summary>
        /// este test prueba lo mismo que el anterior pero debe ser positivo (1)
        /// </summary>
        [Test]
        public void ShipsIsSunkTest2()
        {
            Coords coords2 = new Coords("A1");
            Assert.AreEqual(1, ship.IsSunk());
        }
        /// <summary>
        /// este test prueba que se ingrese contenga el barco... si un barco ocupa A1, A2, A3 y A4 por ejemplo
        /// al ingresar cualquiera de las coordenadas este debe dar positivo (true)
        /// </summary>
        [Test]
        public void  ShipContainCoordTest()
        {
            Coords coords2 = new Coords("A1");
            Assert.AreEqual(true, ship.ShipContainCoord(coords2));
            
        }
        /// <summary>
        /// este test prueba lo mismo que el anterior pero debe ser negativo (false)
        /// </summary>
        [Test]
        public void  ShipContainCoordTest2()
        {
            Coords coords2 = new Coords("B4");
            Assert.AreEqual(false, ship.ShipContainCoord(coords2)); 
        }


        /// <summary>
        /// estos test creo que deberia ir en coords pero no lo tengo claaro
        /// </summary>

        /// <summary>
        /// este test prueba que si ataco a una coordenada contenga el barco cambie de estado y debe dar positivo (true)
        /// </summary>
        [Test]
        public void RecieveDamageTest()
        {
            Coords coords2 = new Coords("A1");
            ship.RecieveDamage(coords2);
            
            Assert.AreEqual(true, coords2.HasBeenAttacked);

        }

        /// <summary>
        /// este test prueba lo mismo que el anterior pero debe ser negativo (false)
        /// </summary>
        [Test]
        public void RecieveDamageTest2()
        {
            Coords coords2 = new Coords("A2");
            ship.RecieveDamage(coords2);
            Assert.AreEqual(null, coords2.HasBeenAttacked);

        }
    }
}