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
        /// <summary>
        /// se que hay atributos que no uso, pero son para poder arreglar en un fututo los test, no se como realziarlos exactamente
        /// </summary>
        User user;
        Gameboard gameboard;
        Coords coords;

        [SetUp]
        public void Setup()
        {
            this.gameboard = new Gameboard(7);
        }

        /// <summary>
        /// se prueba que el metodo traduce una coordenada a un numero
        /// </summary>
        [Test]
        public void TranslateCoordsTest()
        {
            string aCoordsLocation = "C5";
            Assert.AreEqual("24", CoordsTranslate.Translate(aCoordsLocation));
        }

        /// <summary>
        /// se prueba que el metodo traduce una coordenada a un numero en el limite de superior de un tablero de 7
        /// </summary>
        [Test]
        public void TranslateCoordOnLimiteRangeTest()
        {
            string aCoordsLocation = "G7";
            Assert.AreEqual("66", CoordsTranslate.Translate(aCoordsLocation));
        }

        /// <summary>
        /// se prueba que el metodo traduce una coordenada a un numero en el limite de inferior de un tablero
        /// </summary>
        [Test]
        public void TranslateCoordOnLimiteRangeTest2()
        {
            string aCoordsLocation = "A1";
            Assert.AreEqual("00", CoordsTranslate.Translate(aCoordsLocation));
        }


 
    }
}