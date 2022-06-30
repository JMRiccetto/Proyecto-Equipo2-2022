using NUnit.Framework;
using NavalBattle;
using System;

namespace NavalBattle.Test
{
    public class GameboardTests
    {
        private Gameboard gameboard;

        [SetUp]
        public void Setup()
        {    
        }

        //Testea que el tablero se cree con el tamaño correspondiente.
        [Test]
        public void GameboardSideTest()
        {
            gameboard = new Gameboard(6);
            int expected = 6;
            Assert.AreEqual(expected, gameboard.Side);
        }

        //Testea que los barcos validos se agregan correctamente.
        [Test]
        public void AddValidShipsTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            gameboard.AddShip(2, "40", "E");

            int expected = 3;

            Assert.AreEqual(expected, gameboard.Ships.Count);

            Assert.AreEqual("00" ,gameboard.Ships[0].Coords[0].CoordsLocation);

            Assert.AreEqual("10" ,gameboard.Ships[0].Coords[1].CoordsLocation);

            Assert.AreEqual("20" ,gameboard.Ships[0].Coords[2].CoordsLocation);

            Assert.AreEqual("34" ,gameboard.Ships[1].Coords[0].CoordsLocation);

            Assert.AreEqual("33" ,gameboard.Ships[1].Coords[1].CoordsLocation);

            Assert.AreEqual("32" ,gameboard.Ships[1].Coords[2].CoordsLocation);

            Assert.AreEqual("31" ,gameboard.Ships[1].Coords[3].CoordsLocation);

            Assert.AreEqual("40" ,gameboard.Ships[2].Coords[0].CoordsLocation);

            Assert.AreEqual("41" ,gameboard.Ships[2].Coords[1].CoordsLocation);
        }

        //Testea que no se puede agregar un barco con una coordenada inicial no valida.
        [Test]
        public void AddNoValidInitialShipCoordTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            Exception ex = Assert.Throws<InvalidCoordException>(() => gameboard.AddShip(2, "78", "E"));

            Assert.AreEqual("Coordenada no valida.", ex.Message);

            Assert.AreEqual(2, gameboard.Ships.Count);
        }

        //Testea que no se puede agregar un barco con una direccion no valida.
        [Test]
        public void AddNoValidDirectionShipTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            Exception ex = Assert.Throws<Exception>(() => gameboard.AddShip(2, "40", "J"));

            Assert.AreEqual("Direccion no valida.", ex.Message);

            Assert.AreEqual(2, gameboard.Ships.Count);
        }

        //Testea que no se puede agregar un barco que se va del rango del tablero"
        [Test]
        public void AddOutOfRangeShipTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            Exception ex = Assert.Throws<Exception>(() => gameboard.AddShip(9, "40", "E"));

            Assert.AreEqual("Barco fuera de rango.", ex.Message);

            Assert.AreEqual(2, gameboard.Ships.Count);
        }

        //Testea que no se puede agregar un barco que compara al menos una coordenada con un barco ya posicionado.
        [Test]
        public void AddOverlappingShipTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            Exception ex = Assert.Throws<Exception>(() => gameboard.AddShip(2, "00", "E"));

            Assert.AreEqual("Barcos superpuestos.", ex.Message);

            Assert.AreEqual(2, gameboard.Ships.Count);
        }

        //Testea que luego de posicionar los barcos, las coordenadas restantes se agregan a water.
        [Test]
        public void AddWaterValidTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            gameboard.AddShip(2, "40", "E");

            int expected = 27;

            Assert.AreEqual(expected, gameboard.Water.Count);
        }

        //Testea que mientras no se agreguen todos los barcos, water tiene 0 coordenadas.
        [Test]
        public void AddWaterNoValidTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            int expected = 0;

            Assert.AreEqual(expected, gameboard.Water.Count);
        }

        //Testea que las bombas se agreguen correctamente.
        [Test]
        public void AddBombsValidTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddBombs();

            int expected = 3;

            Assert.AreEqual(expected, gameboard.Bombs.Count);
        }

        //Testea que el tablero recibe un ataque valido.
        [Test]
        public void RecieveValidAttackTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            gameboard.AddShip(2, "40", "E");

            Coords coord1 = new Coords("00");

            Coords coord2 = new Coords("01");

            Coords coord3 = new Coords("10");

            Coords coord4 = new Coords("20");

            Assert.AreEqual("Tocado", gameboard.RecieveAttack(coord1));

            Assert.AreEqual("Agua", gameboard.RecieveAttack(coord2));

            Assert.AreEqual("Tocado", gameboard.RecieveAttack(coord3));

            Assert.AreEqual("Hundido", gameboard.RecieveAttack(coord4));
        }

        //Testea que el tablero no puede recibir un ataque a una coordenada no valida.
        [Test]
        public void RecieveInvalidAttackTest1()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");
            
            gameboard.AddShip(4, "34", "W");

            gameboard.AddShip(2, "40", "E");

            Coords coord1 = new Coords("79");

            Exception ex = Assert.Throws<InvalidCoordException>(() => gameboard.RecieveAttack(coord1));

            Assert.AreEqual("Coordenada no valida", ex.Message);
        }

        //Testea que el tablero no puede recibir ataques si no estan posicionados todos los barcos.
        [Test]
        public void RecieveInvalidAttackTest2()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(3, "00", "S");
            
            gameboard.AddShip(4, "34", "W");

            Coords coord1 = new Coords("00");

            Exception ex = Assert.Throws<Exception>(() => gameboard.RecieveAttack(coord1));

            Assert.AreEqual("No estan todos los barcos posicionados.", ex.Message);
        }

        //Testea que cuando se hunden todos los barcos se termina la partida.
        [Test]
        public void IsMatchFinishedTest()
        {
            gameboard = new Gameboard(6);

            gameboard.AddShip(2, "00", "S");

            gameboard.AddShip(2, "01", "S");

            gameboard.AddShip(2, "02", "S");

            Coords coord1 = new Coords("00");

            Coords coord2 = new Coords("10");

            Coords coord3 = new Coords("01");

            Coords coord4 = new Coords("11");

            Coords coord5 = new Coords("02");

            Coords coord6= new Coords("12");

            gameboard.RecieveAttack(coord1);

            gameboard.RecieveAttack(coord2);

            gameboard.RecieveAttack(coord3);

            gameboard.RecieveAttack(coord4);

            gameboard.RecieveAttack(coord5);

            gameboard.RecieveAttack(coord6);

            Assert.AreEqual(true, gameboard.IsMatchFinished());
        }
    }
}