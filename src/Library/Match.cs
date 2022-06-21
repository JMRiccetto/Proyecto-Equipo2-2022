namespace NavalBattle
{
    public class Match
    {
        public readonly GameUser[] Users = new GameUser[2];

        public void AddPlayers(GameUser user1, GameUser user2)
        {
            Users[0] = user1;
            Users[1] = user2;
        }
    }
}