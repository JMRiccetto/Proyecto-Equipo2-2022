using System.Collections.Generic;

namespace NavalBattle
{
    /// <summary>
    /// Clase encargada de crear y contener las partidas en juego, ademáas de agregar usuarios a la lista de espera.
    /// </summary>
    public class Admin
    {
        /// <summary>
        /// Se utiliza singleton para admin.
        /// </summary>
        private static Admin admin;

        /// <summary>
        /// Lista donde se guardan las partidas.
        /// </summary>
        /// <typeparam name="Match">Partida.</typeparam>
        /// <returns></returns>
        private List<Match> matchList = new List<Match>();

        private Admin() { }

        /// <summary>
        /// Singleton de Admin, si no existe una instancia la crea, y si ya existe devuelve esa misma instancia.
        /// </summary>
        /// <returns>Admin.</returns>
        public static Admin getAdmin()
        {
            if (admin == null)
            {
                admin = new Admin();
            }
            return admin;
        }

        /// <summary>
        /// Gets de la lista de partidas en juego.
        /// </summary>
        /// <value></value>
        public List<Match> MatchList
        {
            get
            {
                return this.matchList;
            }
        }

        /// <summary>
        /// Cuando un User busca partida, si en la WaitingList hay un otro User con las mismas características
        /// de partida que el, se emparejan en la misma partida.
        /// Si no hay otro player disponible para jugar, se agrega a waitingList.
        /// </summary>
        /// <param name="user">Usuario.</param>
        public void AddToWaitingList(GameUser user)
        {
            WaitingList.waitingList.Add(user);

            int i = 0;

            while ((i < WaitingList.waitingList.Count - 1) && WaitingList.waitingList[i].Bombs != user.Bombs || WaitingList.waitingList[i].GameboardSide != user.GameboardSide)
            {
                i++;
            }
            if (i < WaitingList.waitingList.Count - 1)
            {
                this.CreateMatch(user, WaitingList.waitingList[i]);
                WaitingList.waitingList.Remove(user);
                WaitingList.waitingList.Remove(WaitingList.waitingList[i]);
            }
        }

        /// <summary>
        /// Se crea la partida con los dos Users luego de ser emparejados.
        /// </summary>
        /// <param name="user1">Usuario 1.</param>
        /// <param name="user2">Usuario 2.</param>
        public void CreateMatch(GameUser user1, GameUser user2)
        {
            Match match = new Match(user1, user2);

            this.matchList.Add(match);
        }
    }
}