using Telegram.Bot;

namespace NavalBattle
{
    /// <summary>
    /// Realizamos un singleton del bot para poder llamar a los metodos del bot de telegram desde los handlers.
    /// </summary>
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