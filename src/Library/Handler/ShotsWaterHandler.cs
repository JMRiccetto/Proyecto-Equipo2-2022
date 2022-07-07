using System;
using System.Text;
using Telegram.Bot.Types;
using System.Linq;
using System.Collections.Generic;

namespace NavalBattle
{
    public class ShotsWaterHandler : BaseHandler
    {
        int x;
        private GameUser user;

        private Match match;
        public ShotsWaterHandler(BaseHandler next) : base(next)
        {
            this.Keywords = new string[] {"/verDisparosAgua"};
        }

        protected override bool InternalHandle(Message message, out string response)
        {
            try
            {
                if (this.CanHandle(message))
                {
                    this.user = UserRegister.Instance.GetUserByNickName(message.From.FirstName.ToString());

                    if (this.user.State != GameUser.UserState.InGame)
                    {
                        throw new InvalidStateException("No puede realizar esta acción en este momento");
                    }

                    foreach (Match match in Admin.getAdmin().MatchList)
                    {
                        if (match.Players.Contains(this.user.Player))
                        {
                            this.match = match;
                        }
                    }

                    if (message.Text.Equals("/verDisparosAgua"))
                    {
                         x =+ this.match.Players[1].Gameboard.Xcount;          
                         x =+ this.match.Players[0].Gameboard.Xcount;
                    }

                    response = ( "el numero de disparos tocados agua: " + x.ToString());
                    return true;
                }

                response = string.Empty;
                return false;
            }
            catch (NullReferenceException ne)
            {
                response = "Ingrese /start para acceder al menu de opciones.";

                return true;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                Cancel();
                response = e.Message;
                return true;
            }
        }

        public override void Cancel()
        {
            this.InternalCancel();
            if (this.Next != null)
            {
                this.Next.Cancel();
            }
        }

        protected override bool CanHandle(Message message)
        {
            if (this.Keywords == null || this.Keywords.Length == 0)
            {
                throw new InvalidOperationException("No hay palabras clave que puedan ser procesadas");
            }

            string[] input = message.Text.Split(" ");

            if (this.Keywords.Contains(input[0]))
            {
                return true;
            }

            return false;
        }
    }
}