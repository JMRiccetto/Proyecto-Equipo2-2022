using System;
using System.Text;

namespace NavalBattle
{
    /// <summary>
    /// Imprime el tablero de manera que se puede visualizar donde se efectuaron los ataques y si algun barco fue tocado.
    /// </summary>
    public class AttackGameboardPrinter : IPrinter
    {
        public StringBuilder PrintGameboard(IGameboardContent gameboardContent)
        {
            StringBuilder s = new StringBuilder();

            string[,] gameboard = gameboardContent.GetGameboardToPrint();

            int lenght = gameboard.GetLength(0);

            for (int x = 0; x < lenght; x++)
            {
                s.Append("      "+x.ToString());
            }

            s.Append("\n");

            for (int i = 0; i < lenght; i++)
            {
                s.Append(i.ToString()+" ");

                for (int j = 0; j < lenght; j++)
                {
                    if (gameboard[i,j] == "t")
                    {
                        s.Append("|  T  |");
                    }
                    else if (gameboard[i,j] == "x")
                    {
                        s.Append("|  X  |");
                    }
                    else 
                    {
                        s.Append("|      |");
                    }
                }
                s.Append("\n");
            }
            return s;
        }
    }
}