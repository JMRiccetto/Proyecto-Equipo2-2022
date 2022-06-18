using NUnit.Framework;
using NavalBattle;

namespace Test.Library
{
    public class UserTests
    {
        User user;

        [SetUp]
        public void Setup()
        {
            this.user = new User("Juan", 7);
        }

        [Test]
        public void GetNameTest()
        {
            Assert.AreEqual("Juan", user.NickName);
        }

        [Test]
        public void NullOrEmptyNameTest()
        {
            
            Assert.False(null, user.NickName);
        }

        [Test]
        public void NewPlayerTest()
        {
            User Fernando = new User("Fernando", 7);
            Assert.AreEqual("Fernando", Fernando.NickName);
            //Assert.AreEqual(7, Fernando.gameboard.Size);
        }

        [Test]
        public void MatchMakingTest()
        {
            //logica del match
        }
    }
}