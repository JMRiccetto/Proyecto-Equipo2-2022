namespace NavalBattle
{
    public class Match
    {
        public readonly User[] Users = new User[2];

        public void AddPlayers(User user1, User user2)
        {
            Users[0] = user1;
            Users[1] = user2;
        }
    }
}

