using Telegram.Bot;

namespace NavalBattle
{
    public class ClientBot
    {
        static private TelegramBotClient getBot;

        private ClientBot() { }

        static public TelegramBotClient GetBot()
        {
            if (getBot == null)
            {
                getBot = new TelegramBotClient("5597909371:AAHrpNZE6JckdcTfOtbMaHEhPBQ6gg9DuqI");
            }
            return getBot;
        }
    }
}