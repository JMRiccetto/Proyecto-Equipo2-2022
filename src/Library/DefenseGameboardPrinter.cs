using System;
using System.Text;

namespace NavalBattle
{
    /// <summary>
    /// Imprime el tablero de manera que se puede visualizar la posicion de los barcos y si fueron tocados o no.
    /// </summary>
    public class DefenseGameboardPrinter : IPrinter
    { 
        public StringBuilder PrintGameboard(IGameboardContent gameboardContent)
        {
            StringBuilder s = new StringBuilder();

            string[,] gameboard = gameboardContent.GetGameboardToPrint();

            int lenght;
            
            lenght= gameboard.GetLength(0);

            s.Append("TABLEROS \n");
            for (int x = 0; x < lenght; x++)
            {
                s.Append("      " +x.ToString());
            }

            s.Append("\n");

            for (int i = 0; i < lenght; i++)
            {
                s.Append(i.ToString() + " ");

                for (int j = 0; j < lenght; j++)
                {
                    if(gameboard[i,j] == "o")
                    {
                        s.Append("|  O  |");
                    }
                    else if(gameboard[i,j] == "t")
                    {
                        s.Append("|  X  |");
                    }
                    else
                    {
                        s.Append("|  ~  |");
                    }
                }
                s.Append("\n");
            }
            return s;
        }
    }
} 