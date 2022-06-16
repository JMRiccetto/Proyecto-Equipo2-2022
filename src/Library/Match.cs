namespace NavalBattle
{
    public class Match
    {
        protected internal Player[] players = new Player[2];

        public void AddPlayers(User user1, User user2)
        {
            Player player1 = new Player(user1);
            Player player2 = new Player(user2);
            players[0] = player1;
            players[1] = player2;
        }
    }
}