using NUnit.Framework;
using Telegram.Bot.Types;
using System.Text;
using NavalBattle;

namespace Test.Library
{
    // Test que demuestra que se pueden registrar usuarios.
    public class PlayerTest
    {
        private GameUser user1;

        private GameUser user2;

        [SetUp]
        public void Setup()
        {
            this.user1 = new GameUser("Juan1", 1);
            this.user2 = new GameUser("Juan2", 2);

            this.user1.GameboardSide = 7;
            this.user1.SearchGame();

            this.user2.GameboardSide = 7;
            this.user2.SearchGame();
        }

        
        /// <summary>
        /// Testea que los barcos se posicionen con sus largos correspondientes(el primero 2, el segundo 3 y el tercero 4).
        /// </summary>
        [Test]
        public void MatchTest1()
        {
            //Checkea al principio de la partida sea el turno de uno de los jugadores.
            Assert.AreEqual(false, this.user1.Player.Turn);
            Assert.AreEqual(true, this.user2.Player.Turn);

            this.user2.Player.PlaceShip("00", "S");
            
            //Checkea que el primer barco que se posiciona sea de largo 2.
            Assert.AreEqual(2, this.user2.Player.Gameboard.Ships[0].Length);

            this.user1.Player.PlaceShip("00", "S");

            this.user2.Player.PlaceShip("01", "S");      

            //Checkea que el segundo barco que se posiciona es de largo 3.
            Assert.AreEqual(3, this.user2.Player.Gameboard.Ships[1].Length);
      
            this.user1.Player.PlaceShip("01", "S");

            this.user2.Player.PlaceShip("02", "S");

            //Checkea que el segundo barco que se posiciona es de largo 4.
            Assert.AreEqual(4, this.user2.Player.Gameboard.Ships[2].Length);

            this.user1.Player.PlaceShip("02", "S");
        }

        /// <summary>
        /// Testea que un jugador puede atacar a otro.
        /// </summary>
        [Test]
        public void AttackTest()
        {
            this.user2.Player.PlaceShip("00", "S");
            this.user1.Player.PlaceShip("00", "S");
            this.user2.Player.PlaceShip("01", "S");      
            this.user1.Player.PlaceShip("01", "S");
            this.user2.Player.PlaceShip("02", "S");
            this.user1.Player.PlaceShip("02", "S");

            string res = this.user2.Player.Attack("00", this.user1.Player.Gameboard);
            
            Assert.AreEqual("Tocado", res);
            Assert.AreEqual(true, this.user1.Player.Gameboard.Ships[0].Coords[0].HasBeenAttacked);
        }

        /// <summary>
        /// Testea que un jugador puede hundir un barco.
        /// </summary>
        [Test]
        public void SunkTest()
        {
            this.user2.Player.PlaceShip("00", "S");
            this.user1.Player.PlaceShip("00", "S");
            this.user2.Player.PlaceShip("01", "S");      
            this.user1.Player.PlaceShip("01", "S");
            this.user2.Player.PlaceShip("02", "S");
            this.user1.Player.PlaceShip("02", "S");

            this.user2.Player.Attack("00", this.user1.Player.Gameboard);
            string res = user2.Player.Attack("10", this.user1.Player.Gameboard);
            
            Assert.AreEqual("Hundido", res);
            Assert.AreEqual(true, this.user1.Player.Gameboard.Ships[0].IsSunk());
        }

        /// <summary>
        /// Testea que el trueno de un jugador cambie al llamar al metodo ChangeTurn.
        /// Aclaración: Los turnos se regulan desde los Handlers. Que un jugador no pueda atacar o posicionar barco
        /// debido a que no es su turno se modifica en el Handler de ataque o de posicionamiento.
        /// </summary>
        [Test]
        public void ChangeTurnTest()
        {
            Assert.AreEqual(false, this.user1.Player.Turn);

            this.user1.Player.ChangeTurn();

            Assert.AreEqual(true, this.user1.Player.Turn);
        }
    }
}