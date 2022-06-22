using System.Collections.Generic;


namespace NavalBattle
{
    public class Admin
    {
        /// <summary>
        /// Lista donde se guardan las partidas.
        /// </summary>
        /// <typeparam name="Match"></typeparam>
        /// <returns></returns>
        List<Match> matchList = new List<Match>();

        public List<Match> MatchList
        {
            get
            {
                return matchList;
            }
        }

        List<string> usersRegister = new List<string>();

    
        /// <summary>
        /// Ser utiliza singleton para admin.
        /// </summary>
        static private Admin admin = null;

        private Admin() { }
        static public Admin getAdmin() 
        {
            if (admin == null)
            {
                admin = new Admin();
            }
            return admin;
        }

        public void UserRegister(string nickname)
        {
            usersRegister.Add(nickname);
        }

        /// <summary>
        /// Cuando un User busca partida, si en la WaitingList hay un otro User con las mismas caracteristicas
        /// de partida que el, se emparejan en la misma partida.
        /// Si no hay otro player disponible para jugar, se agrega a waitingList.
        /// </summary>
        /// <param name="user"></param>
        public void AddToWaitingList(GameUser user)
        {     
            if (WaitingList.waitingList.Count > 0)
            {
                int i = 0;
                while(i< WaitingList.waitingList.Count &&  WaitingList.waitingList[i].GameboardSide != user.GameboardSide && WaitingList.waitingList[i].Bombs != user.Bombs && WaitingList.waitingList[i].DoubleAttack != user.DoubleAttack)
                {
                    i++;
                }
                if (i < WaitingList.waitingList.Count)
                {
                    CreateMatch(user, WaitingList.waitingList[i]);
                    WaitingList.waitingList.Remove(user);
                    WaitingList.waitingList.Remove(WaitingList.waitingList[i]);
                }
                else
                {
                    WaitingList.waitingList.Add(user);
                }
            }
            else
            {
                WaitingList.waitingList.Add(user);
            }
        }
        
        /// <summary>
        /// Se crea la partida con los dos Users luego de ser emparejados.
        /// </summary>
        /// <param name="user1"></param>
        /// <param name="user2"></param>
        public void CreateMatch(GameUser user1, GameUser user2)
        {
            Match match = new Match(user1, user2);

            matchList.Add(match);
        }
    }
}