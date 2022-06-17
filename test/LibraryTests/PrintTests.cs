using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    public class PrintTests
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
        public void GetGameboardToPrintTest()
        {
            Assert.AreEqual(this.player.Gameboard, player.Gameboard.GetGameboardToPrint());
        }

        [Test]
        public void DefenseTest()
        {
            //Assert.AreEqual(7, player.Gameboard.GetGameboardToPrint().GetGameboardToPrint().Length);
        }
    
 
    }
}