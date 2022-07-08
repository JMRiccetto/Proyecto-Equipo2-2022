using System;
using NUnit.Framework;
using NavalBattle;
using System.Collections.Generic;

namespace Tests
{
    [TestFixture]
    public class CountTests
    {
        int expected = 1;
        Count shipCounter = new Count();
        Count waterCounter = new Count();
        Count bombCounter = new Count();
    
        [SetUp]
        public void Setup()
        {

        }

        //Testea que se cambia el estado de una coordenada a atacada.
        [Test]
        public void ShipCountTest()
        {        
            this.shipCounter.AddShipCounter();
            Assert.AreEqual(expected, this.shipCounter.shipCounter);
        }

        [Test]
        public void WaterCountTest()
        {        
            this.waterCounter.AddWaterCounter();
            Assert.AreEqual(expected, this.waterCounter.waterCounter);
        }

        [Test]
        public void BombCountTest()
        {        
            this.bombCounter.AddBombCounter();
            Assert.AreEqual(expected, this.bombCounter.bombCounter);
        }
    }
}