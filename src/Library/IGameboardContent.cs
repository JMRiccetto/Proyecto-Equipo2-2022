namespace NavalBattle
{
    /// <summary>
    /// Interfaz que implementa la clase Gameboard con el objetivo de aplicar DIP.
    /// </summary>
    public interface IGameboardContent
    {
        /// <summary>
        /// Devuelve el tablero en forma de matriz.
        /// </summary>
        /// <returns></returns>
        string[,] GetGameboardToPrint();
    }
} 