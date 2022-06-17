using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    public class PlayerTests
    {
        User user;
        Player player;

        [SetUp]
        public void Setup()
        {
            this.user = new User("Juan");
            this.player = new Player(this.user, 7);
        }

        [Test]
        public void NewPlayerTest()
        {
            Assert.AreEqual(this.player.Gameboard,player.Gameboard);
            Assert.AreEqual(false, this.player.Turn);
        }
    
 
    }
}