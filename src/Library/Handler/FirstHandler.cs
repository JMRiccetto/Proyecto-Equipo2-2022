using System;
using System.Text;

namespace NavalBattle
{
    /// <summary>
    /// Un "handler" del patrón Chain of Responsibility que implementa el comando "hola".
    /// </summary>
    public class FirstHandler : BaseHandler
    {
        public FirstState State { get; set; }

        public FirstHandler()
        {
            this.command = "/start";
            this.State = FirstState.Start;
        }

        protected override bool InternalHandle(IMessage message, out string response)
        {
            try
            {
               if (this.State == FirstState.Start && this.nextHandler != null)
               {
                    StringBuilder menu = new StringBuilder("Bienvenido\n");
                    if ()
               }
            }
        }

        public enum FirstState
        {
            Start,

            AlreadyRegistered
        }
    }
}