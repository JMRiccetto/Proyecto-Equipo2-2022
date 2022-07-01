using Telegram.Bot;

namespace NavalBattle
{
    public class ClientBot
    {
        static private TelegramBotClient getBot = null;

        private ClientBot() { }

        static public ClientBot GetBot()
        {
            if (getBot == null)
            {
                getBot = new TelegramBotClient("5597909371:AAHrpNZE6JckdcTfOtbMaHEhPBQ6gg9DuqI");
            }
            return getBot;
        }
    }
}

