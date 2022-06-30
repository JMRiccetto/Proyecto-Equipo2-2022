using NUnit.Framework;
using Telegram.Bot.Types;
using System.Text;
using NavalBattle;

namespace Test.Library
{
    // Test que demuestra que se pueden registrar usuarios.
    public class UserRegisterTest
    {
        private User user;

        [SetUp]
        public void Setup()
        {
            this.user = new User();
            this.user.FirstName = "Juan";
        }

        // Verifica que la lista esta vacía al empezar el programa.
        [Test]
        public void EmptyListTest()
        {
            Assert.IsEmpty(UserRegister.UserData);
        }

        // Verifica si el usuario no esta registrado y lo registra.
        [Test]
        public void UnregisteredUserTest()
        {
            if(!UserRegister.UserData.Contains(UserRegister.GetUserByNickName(this.user.FirstName)))
                {
                    UserRegister.Instance.CreateUser(this.user.FirstName);
                }
            Assert.IsNotEmpty(UserRegister.UserData);
        }
    }
}