using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NavalBattle
{
    public class ShootTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        //Testea que la cantidad de disparos al agua y a los barcos sean correctos.
        public void ShootWaterTest()
        {
            GameUser user1 = new GameUser("juan1", 1);

            GameUser user2 = new GameUser("juan2", 2);

            //Busqueda de partida.
            user1.SearchGame();

            user2.SearchGame();

            //Posicionamiento de barcos.
            user1.Player.PlaceShip("00", "S");
            user2.Player.PlaceShip("00", "S"); 
            user1.Player.PlaceShip("01", "S");
            user2.Player.PlaceShip("01", "S");   
            user1.Player.PlaceShip("02", "S");
            user2.Player.PlaceShip("02", "S");   
           
            //Testea que al principio Los disparos al agua y a los barcos son 0.
            Assert.AreEqual(0, user1.Player.Disparos.WaterShoots + user2.Player.Disparos.WaterShoots);
            Assert.AreEqual(0, user1.Player.Disparos.ShipShoots + user2.Player.Disparos.ShipShoots);

            //Tocado
            user1.Player.Attack("00", user2.Player.Gameboard);

            //Luego del primer ataque dado a un barco.
            Assert.AreEqual(0, user1.Player.Disparos.WaterShoots + user2.Player.Disparos.WaterShoots);
            Assert.AreEqual(1, user1.Player.Disparos.ShipShoots + user2.Player.Disparos.ShipShoots);
            
            //Tocado
            user2.Player.Attack("00", user2.Player.Gameboard);
            
            //Agua
            user1.Player.Attack("03", user2.Player.Gameboard);

            Assert.AreEqual(1, user1.Player.Disparos.WaterShoots + user2.Player.Disparos.WaterShoots);
            Assert.AreEqual(2, user1.Player.Disparos.ShipShoots + user2.Player.Disparos.ShipShoots);

            //Hundido
            user2.Player.Attack("10", user1.Player.Gameboard);

            Assert.AreEqual(1, user1.Player.Disparos.WaterShoots + user2.Player.Disparos.WaterShoots);
            Assert.AreEqual(3, user1.Player.Disparos.ShipShoots + user2.Player.Disparos.ShipShoots);
            
            //Agua
            user1.Player.Attack("04", user2.Player.Gameboard);

            Assert.AreEqual(2, user1.Player.Disparos.WaterShoots + user2.Player.Disparos.WaterShoots);
            Assert.AreEqual(3, user1.Player.Disparos.ShipShoots + user2.Player.Disparos.ShipShoots);
        }
    }
}