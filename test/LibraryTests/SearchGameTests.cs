using NUnit.Framework;

namespace NavalBattle
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Testea que un jugador pueda buscar partida.
        [Test]
        public void SearchGameTest()
        {
            GameUser user1 = new GameUser("juan1");

            user1.SearchGame(7, false, false);

            Assert.AreEqual("juan1", WaitingList.waitingList[0].NickName);      
        }
    
        //Testea que dos jugadores se puedan emparejar en la misma partida.
        [Test]
        public void MatchmakingTest()
        {
            GameUser user1 = new GameUser("juan1");

            GameUser user2 = new GameUser("juan2");

            user1.SearchGame(7, false, false);

            Assert.IsNull(user1.player);
            
            user2.SearchGame(7, false, false);

            Assert.IsNotNull(user1.player);

            Assert.IsNotNull(user2.player);

            Assert.AreEqual(7, user1.player.Gameboard.Side);

            Assert.AreEqual(7, user2.player.Gameboard.Side);
        } 

        //Testea que se puedan almacenar varias partidas en simultaneo.
        [Test]
        public void MatchListTest()
        {
            GameUser user1 = new GameUser("juan1");

            GameUser user2 = new GameUser("juan2");

            GameUser user3 = new GameUser("juan3");

            GameUser user4 = new GameUser("juan4");

            user1.SearchGame(7, false, false);

            user2.SearchGame(7, false, false);

            user3.SearchGame(6, true, false);
                 
            user4.SearchGame(6, true, false);

            Assert.AreEqual(2, Admin.getAdmin().MatchList.Count);
        }
    }
}