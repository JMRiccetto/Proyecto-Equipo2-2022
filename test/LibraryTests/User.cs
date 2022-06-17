using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    public class UserTests
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
        public void GetNameTest()
        {
            Assert.AreEqual("Juan", user.NickName);
        }

        [Test]
        public void NullNameTest()
        {
            Assert.AreEqual(null, user.NickName);
        }

        [Test]
        public void MatchMakingTest()
        {
            //Assert.AreEqual(null, user.MatchMaking());
        }


 
    }
}