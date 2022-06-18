namespace NavalBattle
{
    /// <summary>
    /// Interfaz que implementa la clase Gameboard con el objetivo de aplicar DIP.
    /// </summary>
    public interface IGameboardContent
    {
        string[,] GetGameboardToPrint();
    }
}