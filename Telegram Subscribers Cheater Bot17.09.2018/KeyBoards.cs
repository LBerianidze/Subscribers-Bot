namespace Telegram_Subscribers_Cheater_Bot17._09._2018
{
    public static class KeyBoards
    {
        public static Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup MainMenu = new Telegram.Bot.Types.ReplyMarkups.ReplyKeyboardMarkup(new[]
        {
            new[]
            {
                new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Купить автопросмотры"),
                new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Купить подписчиков"),
            },
            new[]
            {
                new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Связаться с поддержкой"),
                new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Мои заказы"),
            },
            new[]
            {
                                 new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Демонстрация автопросмотров"),
                new Telegram.Bot.Types.ReplyMarkups.KeyboardButton("Тарифы и F.A.Q"),
            }
           // new[]
           // {
           //     Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Тестовый доступ","TestAccess"),
           //     Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Мои заказы","MyOrders")
           // },
           // new[]
           // {
           //     Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Связаться с поддержкой","ContactSupport"),
           //     Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("F.A.Q.","ShowFaq")
           // }
    }, true);
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup ReturnToMainMenu = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
           {
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Главное меню","MMenu"),
            },
        });
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup AutoViewsAnswers = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
        {
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("1000-2000 🚀","AutoViewSpeed_1000-2000 🚀"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("800-1000 ✈️","AutoViewSpeed_800-1000 ✈️")
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("600-800  🚁","AutoViewSpeed_600-800  🚁"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("500-600 🚗","AutoViewSpeed_500-600 🚗")
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("400-500 🛥️","AutoViewSpeed_400-500 🛥️"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("300-400 🚴","AutoViewSpeed_300-400 🚴")
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("200-300 🏊","AutoViewSpeed_200-300 🏊"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("100-200 🏃","AutoViewSpeed_100-200 🏃")
            },
        });
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup AutoViewSubscribe = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
        {
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("7 суток","AutoViewDays_7"),
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("30 суток","AutoViewDays_30"),
            },
        });
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup AutoViewConfirmation(string QIWILink,string YandexLink)
        {
            return new[]
            {
                new[]
                {
                    Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Оплатить QIWI",QIWILink)
                },
                new[]
                {
                    Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithUrl("Оплатить Yandex",YandexLink)
                },
                new[]
                {
                    Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Отменить","MMenu"),
                },
            };
        }
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup SubscribersCountries = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
{
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Россия","SC_Россия"),
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Иностранные","SC_Иностранные"),
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Любая","SC_Любая"),
            },
        });
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup SubscribersSex = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
{
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Мужской","SS_Мужской"),
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Женский","SS_Женский"),
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("Смешанный","SS_Смешанный"),
            },
        });
        public static Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup TestAccessAnswers = new Telegram.Bot.Types.ReplyMarkups.InlineKeyboardMarkup(new[]
{
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("1000-2000 🚀","TestAccess_1000-2000 🚀"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("800-1000 ✈️","TestAccess_800-1000 ✈️")
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("600-800  🚁","TestAccess_600-800  🚁"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("500-600 🚗","TestAccess_500-600 🚗")
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("400-500 🛥️","TestAccess_400-500 🛥️"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("300-400 🚴","TestAccess_300-400 🚴")
            },
            new[]
            {
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("200-300 🏊","TestAccess_200-300 🏊"),
                Telegram.Bot.Types.ReplyMarkups.InlineKeyboardButton.WithCallbackData("100-200 🏃","TestAccess_100-200 🏃")
            },
        });

    }
}
