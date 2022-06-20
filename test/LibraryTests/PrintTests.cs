using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    /// <summary>
    /// esta clase prueba que las clases que implementen las interface de IPrinter (y como arraste IGameboardContent)
    /// </summary>
    public class PrintTests
    {
        User user;

        [SetUp]
        public void Setup()
        {
            this.user = new User("Juan", 7);
        }

        /// <summary>
        /// este test deberia probar que se imprima el DefenseGameboard
        /// </summary>
        [Test]
        public void DefenseGameboardPrinter()
        {

        }

        /// <summary>
        /// este test deberia probar que se imprima el AttackGameboard
        /// </summary>
        [Test]
        public void AttackGameboardPrinter()
        {
            
        }
    }
}