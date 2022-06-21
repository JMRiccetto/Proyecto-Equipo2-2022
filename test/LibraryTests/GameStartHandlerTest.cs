using NUnit.Framework;
using Telegram.Bot.Types;
using NavalBattle;

namespace Test.Library
{
    /// <summary>
    /// 
    /// </summary>
    public class GameStartHandlerTest
    {
        Message message;

        GameStartHandler handler;

        private GameUser user1;

        private GameUser user2;

        [SetUp]
        public void Setup()
        {
            this.message.Text = new string("");
            this.user1 = new GameUser("Juan");
            this.user2 = new GameUser("Maria");
        }
    }
}