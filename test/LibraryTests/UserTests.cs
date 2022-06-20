using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    /// <summary>
    /// clase de prueba para el user, este seguramente este sujeto a cambios
    /// </summary>
    public class UserTests
    {
        User user;

        /// <summary>
        /// creo un user para las pruebas
        /// </summary>
        [SetUp]
        public void Setup()
        {
            User user = new User("Juan", 7);
        }

        /// <summary>
        /// este test prueba que el nombre del user sea el correcto
        /// </summary>
        [Test]
        public void GetNameTest()
        {
            Assert.AreEqual("Juan", user.NickName);
        }

        /// <summary>
        /// este test prueba que el nombre este vacio o sea null, no se si aplique porque desde la clase lo imposibilito (User)
        /// </summary>
        [Test]
        public void NullOrEmptyNameTest()
        {
            Assert.Fail(null, user.NickName);
        }

        /// <summary>
        /// este test prueba el crear un nuevo usuario y probar que el tablero sea correcto
        /// </summary>
        [Test]
        public void NewPlayerTest()
        {
            User Fernando = new User("Fernando", 7);
            Assert.AreEqual("Fernando", Fernando.NickName);
            //Assert.AreEqual(7, Fernando.gameboard.Size);
        }

        /// <summary>
        /// A definir
        /// </summary>
        [Test]
        public void MatchMakingTest()
        {
            //logica del match
        }
    }
}