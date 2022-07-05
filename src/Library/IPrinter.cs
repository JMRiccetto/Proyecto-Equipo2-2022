using System.Text;

namespace NavalBattle
{
    /// <summary>
    /// Interfaz para la impresión.
    /// </summary>
    public interface IPrinter
    {
        /// <summary>
        /// Método para imprimir tableros, que requiere un objeto del tipo IGameboardContent.
        /// </summary>
        /// <param name="gameboardContent"></param>
        /// <returns></returns>
        public StringBuilder PrintGameboard(IGameboardContent gameboardContent);
    }
}