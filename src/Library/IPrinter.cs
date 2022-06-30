using System;
using System.Text;

namespace NavalBattle
{
    public interface IPrinter
    {
        public StringBuilder PrintGameboard(IGameboardContent gameboardContent);
    }
}