using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace NavalBattle
{
    public class Bot
    {
        private static TelegramBotClient botClient;

        public static TelegramBotClient BotClient()
        {
            if (botClient == null)
            {
                botClient = new TelegramBotClient("5597909371:AAHrpNZE6JckdcTfOtbMaHEhPBQ6gg9DuqI");
            }

            return botClient;
        }
    }
}