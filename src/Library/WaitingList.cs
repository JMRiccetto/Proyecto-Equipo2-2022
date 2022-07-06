using System.Collections.Generic;

namespace NavalBattle
{
    /// <summary>
    /// Lista donde se agregan los usuarios que están esperando para jugar.
    /// </summary>
    public static class WaitingList
    {
        /// <summary>
        /// Lista de espera para los jugadores que no esten en partida.
        /// </summary>
        /// <typeparam name="GameUser">Usuarios.</typeparam>
        /// <returns></returns>
        public static List<GameUser> waitingList = new List<GameUser>();    
    }
}