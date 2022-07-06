namespace NavalBattle
{
    public class Match
    { 
        public readonly Player[] Players = new Player[2];

        public Match(GameUser user1, GameUser user2)
        { 
            this.Players[0] = new Player(user1.GameboardSide, user1.ChatId);
            this.Players[1] = new Player(user2.GameboardSide, user2.ChatId);

            user1.Player = this.Players[0];
            user2.Player = this.Players[1];

            if (user1.Bombs)
            {
                this.Players[0].Gameboard.AddBombs();
                this.Players[1].Gameboard.AddBombs();
            }

            user1.State = GameUser.UserState.InGame;
            user2.State = GameUser.UserState.InGame;

            this.Players[0].ChangeTurn();
        }
    }
}