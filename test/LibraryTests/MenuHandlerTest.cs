using NUnit.Framework;
using Telegram.Bot.Types;
using NavalBattle;

namespace Test.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class MenuHandlerTest
    {
        Message message;

        MenuHandler handler;

        private GameUser user1;

        private GameUser user2;

        [SetUp]
        public void Setup()
        {
            /* this.message.Text = new string("");
            this.user1 = new GameUser("Juan");
            this.user2 = new GameUser("Maria");
            this.handler = new MenuHandler(null); */
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