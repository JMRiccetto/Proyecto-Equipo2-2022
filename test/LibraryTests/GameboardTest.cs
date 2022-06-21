/* using NUnit.Framework;
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
            gameboard = new Gameboard();
            int expected = 6;
            Assert.AreEqual(expected, gameboard.Side);
        }

        //Testea que los barcos validos se agregan correctamente.
        [Test]
        public void AddValidShipsTest()
        {
            gameboard = new Gameboard();

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            gameboard.AddShip(2, "40", "E");

            int expected = 3;

            Assert.AreEqual(expected, gameboard.Ships.Count);
        }

        //Testea que no se puede agregar un barco con una coordenada inicial no valida.
        [Test]
        public void AddNoValidInitialShipCoordTest()
        {
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

            gameboard.AddShip(3, "00", "S");

            gameboard.AddShip(4, "34", "W");

            int expected = 0;

            Assert.AreEqual(expected, gameboard.Water.Count);
        }

        //Testea que las bombas se agreguen correctamente.
        [Test]
        public void AddBombsValidTest()
        {
            gameboard = new Gameboard();

            gameboard.AddBombs();

            int expected = 3;

            Assert.AreEqual(expected, gameboard.Bombs.Count);
        }

        //Testea que el tablero recibe un ataque valido.
        [Test]
        public void RecieveValidAttackTest()
        {
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

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
            gameboard = new Gameboard();

            gameboard.AddShip(3, "00", "S");
            
            gameboard.AddShip(4, "34", "W");

            Coords coord1 = new Coords("00");

            Exception ex = Assert.Throws<Exception>(() => gameboard.RecieveAttack(coord1));

            Assert.AreEqual("No estan todos los barcos posicionados.", ex.Message);
        }
    }
} */