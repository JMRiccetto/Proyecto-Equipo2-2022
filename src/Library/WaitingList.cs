using System;
using System.Collections.Generic;

namespace NavalBattle
{
    /// <summary>
    /// Lista donde se agregan los usuarios que estan esperando para jugar.
    /// </summary>
    public static class WaitingList
    {
        public static List<GameUser> waitingList = new List<GameUser>();    
    }
}