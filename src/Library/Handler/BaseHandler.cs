using System;

namespace NavalBattle
{
    public class BaseHandler : IHandler
    {
        protected IHandler nextHandler;

        protected string command;

        public IHandler SetNext(IHandler handler)
        {
            this.nextHandler = handler;
            return handler;
        }

        public virtual bool InternalHandle(IMessage input, out string response)
        {
            throw new Exception();
        }

        protected virtual bool CanHandle(IMessage input)
        {
            if (this.command == null || this.command.Length == 0)
            {
                throw new InvalidOperationException("No existen palabras claves que puedan ser procesadas");
            }
            return this.command.Equals(input.Text.ToLower().Trim());
        }

        public IHandler Handle(IMessage message, out string response)
        {
            if (this.InternalHandle(message, out response))
            {
                return this;
            }
            else if (this.nextHandler != null)
            {
                return this.nextHandler.Handle(message, out response);
            }
            else
            {
                return null;
            }
        }

        protected virtual void InternalCancel()
        {

        }
    }
}