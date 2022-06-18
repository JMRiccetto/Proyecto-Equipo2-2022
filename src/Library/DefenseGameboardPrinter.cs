using System;
using System.Text;

namespace NavalBattle
{
    /// <summary>
    /// Imprime el tablero de manera que se puede visualizar la posicion de los barcos y si fueron tocados o no.
    /// </summary>
    public class DefenseGameboardPrinter : IPrinter
    { 
        public void PrintGameboard(IGameboardContent gameboardContent)
        {
            StringBuilder s = new StringBuilder();

            string[,] gameboard = gameboardContent.GetGameboardToPrint();

            int lenght;
            
            lenght= gameboard.GetLength(0);

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