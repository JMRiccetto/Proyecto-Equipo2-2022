using System;

namespace NavalBattle
{
    public interface IPrinter
    {
        public void PrintGameboard(IGameboardContent gameboardContent);
    }
}