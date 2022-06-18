using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    /// <summary>
    /// Si no normalizamos el texto en handler y debe realizarse en CoordsTranslate, a estos test desben de agregarse
    /// </summary>
    /// No hice test de OutOfRange porque no deberian llegar a esta instancia.
    public class CoordsTranslateTests
    {
        User user;
        Player player;
        Gameboard gameboard;

        [SetUp]
        public void Setup()
        {
            this.gameboard = new Gameboard(7);
        }

        [Test]
        public void TranslateCoordsTest()
        {
            string aCoordsLocation = "C5";
            Assert.AreEqual("24", CoordsTranslate.Translate(aCoordsLocation));
        }

        [Test]
        public void TranslateCoordOnLimiteRangeTest()
        {
            string aCoordsLocation = "G7";
            Assert.AreEqual("66", CoordsTranslate.Translate(aCoordsLocation));
        }

        [Test]
        public void TranslateCoordOnLimiteRangeTest2()
        {
            string aCoordsLocation = "A1";
            Assert.AreEqual("00", CoordsTranslate.Translate(aCoordsLocation));
        }


 
    }
}