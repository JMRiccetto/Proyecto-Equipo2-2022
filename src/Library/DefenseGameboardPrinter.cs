using System;
using System.Text;

namespace NavalBattle
{
    public class DefenseGameboardPrinter : IPrinter
    {
        public void PrintGameboard(IGameboardContent gameboardContent)
        {
            StringBuilder s = new StringBuilder();

            string[,] gameboard = gameboardContent.GetGameboardToPrint();

            int lenght = gameboard.GetLength(0);

            for (int i = 0; i < lenght; i++)
            {
                for (int j = 0; j < lenght; j++)
                {
                    if(gameboard[i,j] == "o")
                    {
                        s.Append("|o|");
                    }
                    else if(gameboard[i,j] == "t")
                    {
                        s.Append("|x|");
                    }
                    else
                    {
                        s.Append("|~|");
                    }
                }
                s.Append("\n");
            }
            Console.WriteLine(s.ToString());
        }
    }
}