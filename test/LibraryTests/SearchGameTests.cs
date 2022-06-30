using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace NavalBattle
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //Testea que cuando un jugador busca partida va a la lista de espera.
        [Test]
        public void SearchGameTest()
        {
            GameUser user1 = new GameUser("juan1");

            user1.SearchGame();

            Assert.AreEqual("juan1", WaitingList.waitingList[0].NickName);      
        }
    
        //Testea que dos jugadores se puedan emparejar en la misma partida.
        [Test]
        public void MatchmakingTest()
        {
            GameUser user1 = new GameUser("juan1");

            GameUser user2 = new GameUser("juan2");

            user1.SearchGame();

            Assert.IsNull(user1.Player);
            
            user2.SearchGame();

            Assert.IsNotNull(user1.Player);

            Assert.IsNotNull(user2.Player);

            Assert.AreEqual(6, user1.Player.Gameboard.Side);

            Assert.AreEqual(6, user2.Player.Gameboard.Side);
        } 

        //Testea que se puedan almacenar varias partidas en simultaneo.
        [Test]
        public void MatchListTest()
        {
            GameUser user1 = new GameUser("juan1");

            GameUser user2 = new GameUser("juan2");

            GameUser user3 = new GameUser("juan3");

            GameUser user4 = new GameUser("juan4");

            user1.GameboardSide = 7;

            user2.GameboardSide = 7;

            user3.Bombs = true;

            user4.Bombs = true;

            user1.SearchGame();

            user2.SearchGame();

            user3.SearchGame();
                 
            user4.SearchGame();

            Assert.AreEqual(2, Admin.getAdmin().MatchList.Count);
        }
    }
} 