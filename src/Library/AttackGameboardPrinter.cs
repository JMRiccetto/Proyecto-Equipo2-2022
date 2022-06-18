using System;
using System.Text;

namespace NavalBattle
{
    /// <summary>
    /// Imprime el tablero de manera que se puede visualizar donde se efectuaron los ataques y si algun barco fue tocado.
    /// </summary>
    public class AttackGameboardPrinter : IPrinter
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
                    if (gameboard[i,j] == "t")
                    {
                        s.Append("|T|");
                    }
                    else if (gameboard[i,j] == "x")
                    {
                        s.Append("|x|");
                    }
                    else 
                    {
                        s.Append("| |");
                    }
                }
                s.Append("\n");
            }
            Console.WriteLine(s.ToString());
        }
    }
}
