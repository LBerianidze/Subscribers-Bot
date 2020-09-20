using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Telegram_Subscribers_Cheater_Bot17._09._2018
{
    public partial class Form1 : MetroFramework.Forms.MetroForm
    {
        Dictionary<string, string> items = new Dictionary<string, string>();
        public Form1()
        {
            this.InitializeComponent();
            dynamic json = Newtonsoft.Json.JsonConvert.DeserializeObject(System.IO.File.ReadAllText("DB\\Texts.json", Encoding.UTF8));
            foreach (var item in json)
            {
                items.Add(item.Name.ToString(), item.Value.ToString());
            }
            if (System.IO.File.Exists("DB\\users.xml"))
            {
                this.LoadUsers();
            }
            if (System.IO.File.Exists("DB\\completedorders.xml"))
            {
                this.LoadCompletedOrders();
            }
            if (System.IO.File.Exists("DB\\queueorders.xml"))
            {
                this.LoadQueueOrders();
            }
            if (System.IO.File.Exists("DB\\TestAccessUsers.txt"))
            {
                this.TestAccessUsers = System.IO.File.ReadAllLines("DB\\TestAccessUsers.txt").Select(t => Convert.ToInt64(t)).ToList();
            }
            if (System.IO.File.Exists("DB\\TestAccessChannels.txt"))
            {
                this.TestAccessChannels = System.IO.File.ReadAllLines("DB\\TestAccessChannels.txt").ToList();
            }
            if (Properties.Settings.Default != null)
            {
                try
                {
                    this.TelegramBotApiKey.Text = Properties.Settings.Default.TelegramBotKey;
                    this.QIWIApiKey.Text = Properties.Settings.Default.QIWIKey;
                    this.YandexApiKey.Text = Properties.Settings.Default.YandexKey;
                    this.YandexUserIDTextBox.Text = Properties.Settings.Default.YandexUserID;
                    this.AdminIDTextBox.Text = Properties.Settings.Default.UserID;
                    this.GoogleSheetName.Text = Properties.Settings.Default.TableID;
                    this.channelTextBox.Text = Properties.Settings.Default.Channel;
                    this.Channel = this.channelTextBox.Text;
                    this.numericUpDown1.Value = (decimal)Properties.Settings.Default.Subcribers;
                    this.numericUpDown2.Value = (decimal)Properties.Settings.Default.views;
                    this.AdminNameTextBox.Text = Properties.Settings.Default.AdminNameTextBox;
                    this.priceViewsTextBox.Text = Properties.Settings.Default.PriceViewsText;
                    this.priceSubscribersTextBox.Text = Properties.Settings.Default.PriceSubscribersText;
                    this.richTextBox2.Text = Properties.Settings.Default.FAQ;
                }
                catch
                {

                }
            }
        }
        private string Channel { get; set; } = "@smmdustest";
        private object QO = new object();
        private object CO = new object();
        private object SO = new object();
        private object TA = new object();
        private object TS = new object();
        private void LoadUsers()
        {
            var serializer = new XmlSerializer(typeof(List<BotUser>));
            this.users = serializer.Deserialize(new StreamReader("DB\\users.xml", Encoding.UTF8)) as List<BotUser>;
        }
        private void LoadCompletedOrders()
        {
            var serializer = new XmlSerializer(typeof(List<Order>));
            var reader = new StreamReader("DB\\completedorders.xml", Encoding.UTF8);
            this.CompletedOrders = serializer.Deserialize(reader) as List<Order>;
            reader.Close();
        }
        private void LoadQueueOrders()
        {
            var serializer = new XmlSerializer(typeof(List<Order>));
            var reader = new StreamReader("DB\\queueorders.xml", Encoding.UTF8);
            this.OrdersInQueue = serializer.Deserialize(reader) as List<Order>;
            reader.Close();
        }
        private void SaveTestAccessUsers()
        {
            lock (this.TA)
            {
                var sw = new StreamWriter("DB\\TestAccessUsers.txt", false, Encoding.UTF8);
                foreach (var item in this.TestAccessUsers)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }
        private void SaveTestAccessChannels()
        {
            lock (this.TS)
            {
                var sw = new StreamWriter("DB\\TestAccessChannels.txt", false, Encoding.UTF8);
                foreach (var item in this.TestAccessChannels)
                {
                    sw.WriteLine(item);
                }
                sw.Close();
            }
        }
        private void SaveUsers()
        {
            lock (this.SO)
            {
                var serializer = new XmlSerializer(typeof(List<BotUser>));
                var writer = new XmlTextWriter("DB\\users.xml", Encoding.UTF8)
                {
                    Formatting = Formatting.Indented,
                    IndentChar = '\t',
                    Indentation = 1,
                    QuoteChar = '\''
                };
                serializer.Serialize(writer, this.users);
                writer.Close();
            }
        }
        private void SaveCompletedOrders()
        {
            lock (this.CO)
            {
                var serializer = new XmlSerializer(typeof(List<Order>));
                var writer = new XmlTextWriter("DB\\completedorders.xml", Encoding.UTF8)
                {
                    Formatting = Formatting.Indented,
                    IndentChar = '\t',
                    Indentation = 1,
                    QuoteChar = '\''
                };
                serializer.Serialize(writer, this.CompletedOrders);
                writer.Close();
            }
        }
        private void SaveQueueOrders()
        {
            lock (this.QO)
            {
                var serializer = new XmlSerializer(typeof(List<Order>));
                var writer = new XmlTextWriter("DB\\queueorders.xml", Encoding.UTF8)
                {
                    Formatting = Formatting.Indented,
                    IndentChar = '\t',
                    Indentation = 1,
                    QuoteChar = '\''
                };
                serializer.Serialize(writer, this.OrdersInQueue);
                writer.Close();
            }
        }
        private TelegramBotClient client;
        private List<BotUser> users = new List<BotUser>();
        private LBQIWIApi.QiwiApi api;
        private LBYandexMoneyApi.YandexMoneyApi YandexApi;
        private List<Order> OrdersInQueue = new List<Order>();
        private List<Order> CompletedOrders = new List<Order>();
        private GoogleSheetTableWorker worker;
        private List<long> TestAccessUsers = new List<long>();
        private List<string> TestAccessChannels = new List<string>();
        private long YandexUserID;
        private long AdminID;
        private string AdminName;
        private bool testmode;
        private string priceViewsText;
        private string priceSubscribersText;
        private string FAQ;

        private void RunButton_Click(object sender, EventArgs e)
        {
            Timer_Elapsed(null, null);
            this.testmode = this.checkBox1.Checked;
            //this.worker = new GoogleSheetTableWorker(this.GoogleSheetName.Text);
            this.AdminID = Convert.ToInt64(this.AdminIDTextBox.Text);
            this.AdminName = this.AdminNameTextBox.Text;
            this.YandexUserID = Convert.ToInt64(this.YandexUserIDTextBox.Text);
            this.priceViewsText = this.priceViewsTextBox.Text;
            this.priceSubscribersText = this.priceSubscribersTextBox.Text;
            this.FAQ = this.richTextBox2.Text;

            //this.client = new TelegramBotClient(this.TelegramBotApiKey.Text);
            //this.client.OnMessage += this.Client_OnMessage;
            //this.client.OnCallbackQuery += this.Client_OnCallbackQuery;
            var Administrator = this.client.GetMeAsync().Result;
            this.api = new LBQIWIApi.QiwiApi(this.QIWIApiKey.Text, 10000, LBQIWIApi.HistoryType.IN);
            this.api.OnHistoryChange += this.Api_OnHistoryChange;
            this.api.Start();
            this.YandexApi = new LBYandexMoneyApi.YandexMoneyApi(this.YandexApiKey.Text, 10000, LBYandexMoneyApi.HistoryType.IN);
            this.YandexApi.OnHistoryChange += this.YandexApi_OnHistoryChange;
            this.YandexApi.Start();
            //var history = await this.YandexApi.GetHistory();//Получении истории последних платежей
            this.BotStatus.Text = "Бот запущен";
            //this.client.StartReceiving();
            this.BotStatus.ForeColor = System.Drawing.Color.Lime;
            var timer = new System.Timers.Timer
            {
                Interval = 60000
            };
            timer.Elapsed += this.Timer_Elapsed;
            timer.Start();
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                var itemsToDelete = this.OrdersInQueue.Where(t => (DateTime.Now - t.AddDate).TotalMinutes > 4320).ToList();
                for (var i = 0; i < itemsToDelete.Count; i++)
                {
                    this.OrdersInQueue.Remove(itemsToDelete[i]);
                }
                this.SaveQueueOrders();
                itemsToDelete = new List<Order>();
                for (var i = 0; i < this.CompletedOrders.Count; i++)
                {
                    var item = this.CompletedOrders[i];
                    if (item.Mode == Mode.AutoView)
                    {
                        var timespan = (DateTime.Now - item.ActivateDate).TotalMinutes;

                        if (item.Answers[3] == "7")
                        {
                            if (timespan > 10080)
                            {
                                itemsToDelete.Add(item);
                                await this.client.SendTextMessageAsync(item.UserID, $"Напоминание. Ваша подписка на просмотры на канал {item.Answers[0]} закончилась. Можете продлить её в боте.");
                            }
                            else if (timespan > 8640 && !item.Notified)
                            {
                                await this.client.SendTextMessageAsync(item.UserID, $"Напоминание. Ваша подписка на просмотры на канал {item.Answers[0]} закончится через 24 часа.");

                                item.Notified = true;
                            }
                        }
                        else if (item.Answers[3] == "30")
                        {
                            if (timespan > 43200)
                            {
                                itemsToDelete.Add(item);
                                await this.client.SendTextMessageAsync(item.UserID, $"Напоминание. Ваша подписка на просмотры на канал {item.Answers[0]} закончилась. Можете продлить её в боте.");
                            }
                            else if (timespan > 41760 && !item.Notified)
                            {
                                await this.client.SendTextMessageAsync(item.UserID, $"Напоминание. Ваша подписка на просмотры на канал {item.Answers[0]} закончится через 24 часа.");

                                item.Notified = true;

                            }
                        }
                    }
                    else
                    {
                        if (Int32.TryParse(item.Answers[1], out var count) && Int32.TryParse(item.Answers[2], out var speed))
                        {
                            decimal hours = (count / speed);
                            var afterCreatHours = (decimal)(DateTime.Now - item.ActivateDate).TotalHours;
                            if (afterCreatHours > hours + 24)
                            {
                                itemsToDelete.Add(item);
                                await this.client.SendTextMessageAsync(item.UserID, $"Напоминание. Накрутка подписчиков на канал {item.Answers[0]} завершена.");
                            }
                            else if (afterCreatHours > hours && !item.Notified)
                            {
                                item.Notified = true;
                            }
                        }
                    }
                }
                for (var z = 0; z < itemsToDelete.Count; z++)
                {
                    this.CompletedOrders.Remove(itemsToDelete[z]);
                }
                this.SaveCompletedOrders();
            }
            catch(Exception ex)
            {
                System.IO.File.WriteAllText("timer_error.txt", "Timer method error. Message: " + ex.Message);
            }
        }

        private void YandexApi_OnHistoryChange(Yandex.Money.Api.Sdk.Responses.OperationDetailsResult pay)
        {
            try
            {
                var andexResultErrorWriter = new StreamWriter("Yandex_AllReceivedPayments.log", true, Encoding.UTF8);
                andexResultErrorWriter.WriteLine($"Поступил платеж с ID {pay.OperationId},сообщением {pay.Message},суммой {pay.Amount},комментарием {pay.Comment},заголовком {pay.Title} в {DateTime.Now.ToString()}");
                andexResultErrorWriter.Close();
                if (pay.Message.StartsWith("LBPL"))
                {
                    var completedcount = this.CompletedOrders.Count;
                    var l = Convert.ToInt64(pay.Message.Trim().Substring(4, pay.Message.Length - 4).Replace("Z", ""));//Очищаем от лишних знаком,убираем префикс LBPL и символ Z 
                    var order = this.OrdersInQueue.Find(t => t.ID == l);
                    if (order == null)
                    {

                        var notFoundOrderLogWriter = new StreamWriter("Yandex_NotFoundOrders.log", true, Encoding.UTF8);
                        notFoundOrderLogWriter.WriteLine($"Заказ с ID {l} не найден в очереди заказов");
                        notFoundOrderLogWriter.Close();
                    }
                    else
                    {
                        if (pay.Amount >= order.MoneyCount)
                        {
                            this.client.SendTextMessageAsync(new ChatId(order.UserID), "Подписка активирована!\nВы можете посмотреть все активные подписки нажав на кнопку \"Заказы\"");
                            order.ActivateDate = DateTime.Now;
                            this.CompletedOrders.Add(order);
                            this.client.SendTextMessageAsync(this.AdminID, String.Join("\n", order.Answers));
                            if (order.Mode == Mode.AutoView)
                            {
                                this.worker.UpdateTable(0, order.Answers);
                            }
                            else if (order.Mode == Mode.Subscribers)
                            {
                                this.worker.UpdateTable(1, order.Answers);
                            }
                            var user = this.users.Find(t => t.ID == order.UserID);
                            if (user != null)
                            {
                                this.client.DeleteMessageAsync(order.UserID, user.LastMessage);
                                user.LastMessage = 0;
                            }
                        }
                        else
                        {
                            var notFoundOrderLogWriter = new StreamWriter("Yandex_NotEnoughMoney.log", true, Encoding.UTF8);
                            notFoundOrderLogWriter.WriteLine($"Стоимость заказа с ID {l} {order.MoneyCount},получено {pay.Amount}");
                            notFoundOrderLogWriter.Close();
                            this.client.SendTextMessageAsync(new ChatId(order.UserID), "Возникла ошибка во время проверки платежа.\nЦена не соответствует требуемой.\nСрочно обратитесь к службы поддерки!");
                        }
                        this.OrdersInQueue.Remove(order);
                        this.SaveQueueOrders();
                        if (completedcount != this.CompletedOrders.Count)
                        {
                            this.SaveCompletedOrders();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var andexResultErrorWriter = new StreamWriter("Yandex_ThrowedErrors.log", true, Encoding.UTF8);
                andexResultErrorWriter.WriteLine(pay.Message + "  " + pay.Amount + "  " + pay.Comment + "  " + ex.Message + "  " + DateTime.Now.ToString());
                andexResultErrorWriter.Close();
            }
        }
        private void Api_OnHistoryChange(LBQIWIApi.QiwiPayment pay)
        {
            try
            {
                if (pay.Comment.StartsWith("LBPL_"))
                {
                    var completedcount = this.CompletedOrders.Count;
                    var l = Convert.ToInt64(pay.Comment.Substring(5, pay.Comment.Length - 5).Replace(")", ""));
                    var order = this.OrdersInQueue.Find(t => t.ID == l);
                    if (order != null)
                    {
                        if (pay.sum.Amount >= order.MoneyCount)
                        {
                            this.client.SendTextMessageAsync(new ChatId(order.UserID), "Подписка активирована!\nВы можете посмотреть все активные подписки нажав на кнопку \"Заказы\"");
                            order.ActivateDate = DateTime.Now;
                            this.CompletedOrders.Add(order);
                            this.client.SendTextMessageAsync(this.AdminID, String.Join("\n", order.Answers));
                            if (order.Mode == Mode.AutoView)
                            {
                                this.worker.UpdateTable(0, order.Answers);
                            }
                            else if (order.Mode == Mode.Subscribers)
                            {
                                this.worker.UpdateTable(1, order.Answers);
                            }
                            var user = this.users.Find(t => t.ID == order.UserID);
                            this.client.DeleteMessageAsync(order.UserID, user.LastMessage);
                            user.LastMessage = 0;
                        }
                        else
                        {
                            this.client.SendTextMessageAsync(new ChatId(order.UserID), "Возникла ошибка во время проверки платежа.\nЦена не соответствует требуемой.\nСрочно обратитесь к службы поддерки!");
                        }
                        this.OrdersInQueue.Remove(order);
                        this.SaveQueueOrders();
                        if (completedcount != this.CompletedOrders.Count)
                        {
                            this.SaveCompletedOrders();
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private async void Client_OnCallbackQuery(object sender, Telegram.Bot.Args.CallbackQueryEventArgs e)
        {
            try
            {
                var user = this.users.Find(t => t.ID == e.CallbackQuery.Message.Chat.Id);
                if (user == null)
                {
                    return;
                }
                if (this.testmode && user.ID != 528786160)
                {
                    return;
                }
                var Data = e.CallbackQuery.Data;
                if (Data == "MMenu")
                {
                    user.Mode = Mode.None;
                    user.Step = Step.Zero;
                    if (user.LastMessage != 0)
                    {
                        await this.client.DeleteMessageAsync(user.ID, user.LastMessage);
                    }

                    user.LastMessage = 0;
                    await this.client.SendTextMessageAsync(user.ID, "Заказ отменен", replyMarkup: KeyBoards.MainMenu);
                }
                else if (Data.StartsWith("AutoViewSpeed_") && user.Step == Step.Three && user.Mode == Mode.AutoView)
                {
                    var sp = Data.Substring(14, Data.Length - 14);
                    user.Step = Step.Four;
                    user.Order.Answers[2] = sp;
                    await this.client.SendTextMessageAsync(user.ID, "Выберите период подписки", replyMarkup: KeyBoards.AutoViewSubscribe);
                }
                else if (Data.StartsWith("AutoViewDays_") && user.Step == Step.Four && user.Mode == Mode.AutoView)
                {
                    var sp = Data.Substring(13, Data.Length - 13);
                    user.Step = Step.Five;
                    user.Order.Answers[3] = sp;
                    user.Order.MoneyCount = (float)Math.Floor(this.GetAutoViewsPrice(user.Order.Answers));
                    user.Order.AddDate = DateTime.Now;
                    var QIWILink = this.api.CreateQIWIPayLink(user.Order.MoneyCount, "LBPL_" + user.Order.ID.ToString().Insert(8, ")"));
                    var YandexLink = $"https://money.yandex.ru/to/{this.YandexUserID}/{user.Order.MoneyCount}";
                    user.LastMessage = (await this.client.SendTextMessageAsync(user.ID, "Подтвердите заказ.\n" +
                             $"Продвигаемый канал: {user.Order.Answers[0]}\n" +
                             $"Число просмотров: {user.Order.Answers[1]}\n" +
                             $"Время выполнения (в часах): {user.Order.Answers[2]}\n" +
                             $"Период подписки: {user.Order.Answers[3]} суток\n" +
                             $"Стоимость: {user.Order.MoneyCount} руб\n\n" +
                             $"❗️❗️❗️Комментарий Yandex платежа:❗❗❗\nLBPL{user.Order.ID.ToString().Insert(8, "Z")}", replyMarkup: KeyBoards.AutoViewConfirmation(QIWILink, YandexLink))).MessageId;
                    this.OrdersInQueue.Add((Order)user.Order.Clone());
                }
                else if (Data.StartsWith("SC_") && user.Step == Step.Four && user.Mode == Mode.Subscribers)
                {
                    var sp = Data.Substring(3, Data.Length - 3);
                    user.Step = Step.Five;
                    user.Order.Answers[3] = sp;
                    await this.client.SendTextMessageAsync(user.ID, "Выберите пол подписчиков", replyMarkup: KeyBoards.SubscribersSex);
                    this.SaveQueueOrders();
                }
                else if (Data.StartsWith("SS_") && user.Step == Step.Five && user.Mode == Mode.Subscribers)
                {
                    var sp = Data.Substring(3, Data.Length - 3);
                    user.Step = Step.Six;
                    user.Order.Answers[4] = sp;
                    user.Order.MoneyCount = (float)Math.Floor(this.GetSubscribersPrice(user.Order.Answers));
                    user.Order.AddDate = DateTime.Now;
                    var QIWILink = this.api.CreateQIWIPayLink(user.Order.MoneyCount, "LBPL_" + user.Order.ID.ToString().Insert(8, ")"));
                    var YandexLink = $"https://money.yandex.ru/to/{this.YandexUserID}/{user.Order.MoneyCount}";
                    user.LastMessage = (await this.client.SendTextMessageAsync(user.ID, "Подтвердите заказ.\n" +
                               $"Продвигаемый канал: {user.Order.Answers[0]}\n" +
                               $"Количество подписчиков: {user.Order.Answers[1]}\n" +
                               $"Скорость накрутки: {user.Order.Answers[2]}\n" +
                               $"Страна подписчиков: {user.Order.Answers[3]}\n" +
                               $"Пол: {user.Order.Answers[4]}\n" +
                               $"Стоимость: {user.Order.MoneyCount} руб.\n\n" +
                               $"❗️❗️❗️Комментарий Yandex платежа:❗️❗️❗️\n{"LBPL" + user.Order.ID.ToString().Insert(8, "Z")}", replyMarkup: KeyBoards.AutoViewConfirmation(QIWILink, YandexLink))).MessageId;
                    this.OrdersInQueue.Add((Order)user.Order.Clone());
                    this.SaveQueueOrders();
                }
                else if (Data.StartsWith("TestAccess_") && user.Step == Step.Three && user.Mode == Mode.TestAccess)
                {
                    var sp = Data.Substring(11, Data.Length - 11);
                    user.Step = Step.Four;
                    user.Order.Answers[2] = sp;
                    await this.client.SendTextMessageAsync(user.ID, "Тестовый период будет включен на 2 часа");
                    await this.client.SendTextMessageAsync(this.AdminID, String.Join("\n", user.Order.Answers));

                    this.TestAccessUsers.Add(user.ID);
                    this.SaveTestAccessUsers();
                    this.TestAccessChannels.Add(user.Order.Answers[0]);
                    this.SaveTestAccessChannels();
                    this.worker.UpdateTable(2, user.Order.Answers);
                }
                await this.client.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
            }
            catch (Exception)
            {
            }
        }
        private async void Client_OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            try
            {
                var user = this.users.Find(t => t.ID == e.Message.Chat.Id);
                var Message = e.Message;
                if (Message.Text.StartsWith("/start"))
                {
                    if (user == null)
                    {
                        #region longmessage
                        this.users.Add(new BotUser() { ID = Message.Chat.Id });
                        this.SaveUsers();
                        await this.client.SendTextMessageAsync(Message.Chat.Id, 
                            items["Greeting"].Replace("{USERNAME}", Message.Chat.FirstName).Replace("{ADMINNAME}", this.AdminName), Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: KeyBoards.MainMenu);
                        #endregion
                    }
                    else
                    {
                        await this.client.SendTextMessageAsync(Message.Chat.Id, "Главное меню!", Telegram.Bot.Types.Enums.ParseMode.Markdown, replyMarkup: KeyBoards.MainMenu);
                    }
                }
                else if (Message.Text == "Купить автопросмотры")
                {
                    user.Mode = Mode.AutoView;
                    user.Step = Step.One;
                    await this.client.SendTextMessageAsync(user.ID, items["BuyViews"]);
                }
                else if (Message.Text == "Купить подписчиков")
                {
                    user.Mode = Mode.Subscribers;
                    user.Step = Step.One;
                    await this.client.SendTextMessageAsync(user.ID, items["BuySubscribers"]);
                }
                else if (Message.Text == "Протестировать автопросмотры")
                {
                    user.Mode = Mode.TestAccess;
                    user.Step = Step.One;
                    await this.client.SendTextMessageAsync(user.ID, "Укажите продвигаемый канал 📲\n\n" +
            $"Пример: {this.Channel} - для открытого канала\n" +
            "https://t.me/joinchat/AAAAAEqdD344fFdsF - для закрытого канала");
                }
                else if (Message.Text == "Мои заказы")
                {
                    var userorders = this.CompletedOrders.Where(t => t.UserID == user.ID).ToList();
                    if (userorders.Count == 0)
                    {
                        await this.client.SendTextMessageAsync(user.ID, "У Вас нет купленных подписок!");
                    }
                    else
                    {
                        foreach (var item in userorders)
                        {
                            var dt = new DateTime(item.ActivateDate.Year, item.ActivateDate.Month,
                              item.ActivateDate.Day, item.ActivateDate.Hour, item.ActivateDate.Minute, 0);
                            if (item.Mode == Mode.AutoView)
                            {
                                dt = dt.AddDays(Convert.ToDouble(item.Answers[3]));
                                await this.client.SendTextMessageAsync(user.ID, "Автопросмотры:\n" +
                                    $"Продвигаемый канал: {item.Answers[0]}\n" +
                                    $"Число просмотров: {item.Answers[1]}\n" +
                                    $"Время выполнения (в часах): {item.Answers[2]}\n" +
                                    $"Период подписки: {item.Answers[3]}\n" +
                                    $"Стоимость:  {item.MoneyCount}\n\n" +
                                    $"Действителен до: " + dt.ToString());
                            }
                            else if (item.Mode == Mode.Subscribers)
                            {
                                await this.client.SendTextMessageAsync(user.ID, "Подписчики:\n" +
                                   $"Продвигаемый канал: {item.Answers[0]}\n" +
                                   $"Количество подписчиков: {item.Answers[1]}\n" +
                                   $"Время выполнения (в часах): {item.Answers[2]}\n" +
                                   $"Стоимость:  {item.MoneyCount}");
                            }
                        }
                    }
                }
                else if (Message.Text == "Связаться с поддержкой")
                {
                    await this.client.SendTextMessageAsync(user.ID, "Для того чтобы связаться с поддержкой, напишите в Telegram поддержки: " + this.AdminName);
                }
                else if (Message.Text == "Тарифы и F.A.Q")
                {
                    await this.client.SendTextMessageAsync(user.ID, this.FAQ);
                }
                else if (Message.Text == "Демонстрация автопросмотров")
                {
                    await this.client.SendTextMessageAsync(user.ID, $"Перейдите на канал {this.Channel} для ознакомления с работоспособностью автопросмотров.");
                }
                else if (user.Mode == Mode.AutoView && user.Step == Step.One)
                {
                    user.Order = new Order
                    {
                        UserID = user.ID,
                        Mode = user.Mode,
                        ID = DateTime.Now.Ticks,
                        Answers = new string[4]
                    };
                    user.Order.Answers[0] = Message.Text;
                    user.Step = Step.Two;
                    await this.client.SendTextMessageAsync(user.ID, items["ViewsCount"]);
                }
                else if (user.Mode == Mode.AutoView && user.Step == Step.Two)
                {
                    if (Int32.TryParse(Message.Text, out var V) && V >= 1 && V <= 100000)
                    {
                        user.Order.Answers[1] = Message.Text;
                        user.Step = Step.Three;
                        await this.client.SendTextMessageAsync(user.ID, "Укажите количество часов, за которое будет накручиваться каждый пост (выберите 0, если нужна максимальная скорость).");
                    }
                    else
                    {
                        await this.client.SendTextMessageAsync(user.ID, "Сообщение должно содержать числовое значение от 1 до 100 000. Попробуйте еще раз.");
                    }
                }
                else if (user.Mode == Mode.AutoView && user.Step == Step.Three)
                {
                    if (Int32.TryParse(Message.Text, out var V) && V >= 0 && V <= 5000)
                    {
                        user.Step = Step.Four;
                        user.Order.Answers[2] = Message.Text;
                        await this.client.SendTextMessageAsync(user.ID, "Выберите период подписки", replyMarkup: KeyBoards.AutoViewSubscribe);
                    }
                    else
                    {
                        await this.client.SendTextMessageAsync(user.ID, "Сообщение должно содержать числовое значение от 0 до 5000. Попробуйте еще раз.");
                    }
                }
                else if (user.Mode == Mode.Subscribers && user.Step == Step.One)
                {
                    user.Order = new Order
                    {
                        UserID = user.ID,
                        Mode = user.Mode,
                        ID = DateTime.Now.Ticks,
                        Answers = new string[5]
                    };
                    user.Order.Answers[0] = Message.Text;
                    user.Step = Step.Two;
                    await this.client.SendTextMessageAsync(user.ID, "Укажите количество подписчиков для накрутки (не менее 50).\n\n" + this.priceSubscribersText);
                }
                else if (user.Mode == Mode.Subscribers && user.Step == Step.Two)
                {
                    if (Int32.TryParse(Message.Text, out var V) && V >= 50 && V <= 150000)
                    {
                        user.Order.Answers[1] = Message.Text;
                        user.Step = Step.Three;
                        await this.client.SendTextMessageAsync(user.ID, "Укажите количество часов, за которое будут накручены подписчики (выберите 0, если нужна максимальная скорость)");
                    }
                    else
                    {
                        await this.client.SendTextMessageAsync(user.ID, "Сообщение должно содержать числовое значение от 50 до 150 000. Попробуйте еще раз.");
                    }
                }
                else if (user.Mode == Mode.Subscribers && user.Step == Step.Three)
                {
                    if (Int32.TryParse(Message.Text, out var V))
                    {
                        user.Order.Answers[2] = Message.Text;
                        user.Order.MoneyCount = (float)Math.Floor(this.GetSubscribersPrice(user.Order.Answers));
                        user.Order.AddDate = DateTime.Now;
                        var QIWILink = this.api.CreateQIWIPayLink(user.Order.MoneyCount, "LBPL_" + user.Order.ID.ToString().Insert(8, ")"));
                        var YandexLink = $"https://money.yandex.ru/to/{this.YandexUserID}/{user.Order.MoneyCount}";
                        user.LastMessage = (await this.client.SendTextMessageAsync(user.ID, "Подтвердите заказ.\n" +
                                   $"Продвигаемый канал: {user.Order.Answers[0]}\n" +
                                   $"Количество подписчиков: {user.Order.Answers[1]}\n" +
                                   $"Время выполнения (в часах): {user.Order.Answers[2]}\n" +
                                   $"Стоимость: {user.Order.MoneyCount} руб.\n\n" +
                                   $"❗️❗️❗️Комментарий Yandex платежа:❗️❗️❗️\n{"LBPL" + user.Order.ID.ToString().Insert(8, "Z")}", replyMarkup: KeyBoards.AutoViewConfirmation(QIWILink, YandexLink))).MessageId;
                        this.OrdersInQueue.Add((Order)user.Order.Clone());
                        this.SaveQueueOrders();
                        //user.Step = Step.Four;
                        //this.client.SendTextMessageAsync(user.ID, "Выберите страну подписчиков", replyMarkup: KeyBoards.SubscribersCountries);
                    }
                    else
                    {
                        await this.client.SendTextMessageAsync(user.ID, "Сообщение должно содержать числовое значение. Попробуйте еще раз.");
                    }
                }
                else if (user.Mode == Mode.TestAccess && user.Step == Step.One)
                {
                    user.Order = new Order
                    {
                        Answers = new string[3]
                    };
                    user.Order.Answers[0] = Message.Text;
                    user.Step = Step.Two;
                    if (this.TestAccessUsers.Find(t => t == Message.Chat.Id) == 0 && this.TestAccessChannels.Find(t => t == Message.Text) == null)
                    {
                        await this.client.SendTextMessageAsync(user.ID, "👀 Укажите число просмотров на каждый пост, до которого будет накручиваться каждый пост вашего канала (+/- 10%)\n\n" +
                            "Пример: если на посту уже есть 500 просмотров, а вам нужно, чтобы было 1500, то нужно указывать 1500, а не 1000. ");
                    }
                    else
                    {
                        user.Order.Answers = null;
                        user.Mode = Mode.None;
                        user.Step = Step.Zero;
                        await this.client.SendTextMessageAsync(user.ID, "Вы прежде уже активировали тестовую подписку или для данного канала уже был использован тестовый доступ!");
                    }
                }
                else if (user.Mode == Mode.TestAccess && user.Step == Step.Two)
                {
                    if (Int32.TryParse(Message.Text, out var V))
                    {
                        user.Order.Answers[1] = Message.Text;
                        user.Step = Step.Three;
                        await this.client.SendTextMessageAsync(user.ID, "Выберите скорость накрутки просмотров (число просмотров в час)", replyMarkup: KeyBoards.TestAccessAnswers);
                    }
                    else
                    {
                        await this.client.SendTextMessageAsync(user.ID, "Сообщение должно быть в виде целого числа.\nПожалуйста,введите еще раз");
                    }
                }
            }
            catch
            {

            }
        }
        private float GetSubscribersPrice(string[] array)
        {
            float Price = 0;
            var SubCount = Convert.ToInt32(array[1]);
            if (SubCount <= 1000)
            {
                Price = 1;
            }
            else if (SubCount <= 10000)
            {
                Price = 0.9f;
            }
            else
            {
                Price = 0.8f;
            }
            return Price * Properties.Settings.Default.Subcribers * SubCount;
        }
        private float GetAutoViewsPrice(string[] array)
        {
            var Price = 0;
            var ViewsCount = Convert.ToInt32(array[1]);
            var days = Convert.ToInt32(array[3]);
            if (ViewsCount <= 5000)
            {
                Price = days == 7 ? 1000 : 3000;
            }
            else if (ViewsCount <= 10000)
            {
                Price = days == 7 ? 1200 : 3500;
            }
            else if (ViewsCount <= 15000)
            {
                Price = days == 7 ? 1500 : 4500;
            }
            else if (ViewsCount <= 20000)
            {
                Price = days == 7 ? 1650 : 5000;
            }
            else if (ViewsCount <= 25000)
            {
                Price = days == 7 ? 1800 : 5500;
            }
            else if (ViewsCount <= 30000)
            {
                Price = days == 7 ? 2100 : 6500;
            }
            else if (ViewsCount <= 40000)
            {
                Price = days == 7 ? 2500 : 8500;
            }
            else if (ViewsCount <= 50000)
            {
                Price = days == 7 ? 3100 : 10500;
            }
            else if (ViewsCount <= 100000)
            {
                Price = days == 7 ? 4100 : 14500;
            }
            else
            {
                Price = days == 7 ? 6100 : 17500;
            }
            return Price * Properties.Settings.Default.views;
        }
        private void TelegramBotApiKey_Leave(object sender, EventArgs e)
        {
            Properties.Settings.Default.AdminNameTextBox = this.AdminNameTextBox.Text;
            Properties.Settings.Default.TelegramBotKey = this.TelegramBotApiKey.Text;
            Properties.Settings.Default.QIWIKey = this.QIWIApiKey.Text;
            Properties.Settings.Default.YandexKey = this.YandexApiKey.Text;
            Properties.Settings.Default.YandexUserID = this.YandexUserIDTextBox.Text;
            Properties.Settings.Default.UserID = this.AdminIDTextBox.Text;
            Properties.Settings.Default.TableID = this.GoogleSheetName.Text;
            Properties.Settings.Default.Channel = this.channelTextBox.Text;
            Properties.Settings.Default.Subcribers = (float)this.numericUpDown1.Value;
            Properties.Settings.Default.views = (float)this.numericUpDown2.Value;
            Properties.Settings.Default.PriceViewsText = this.priceViewsTextBox.Text;
            Properties.Settings.Default.PriceSubscribersText = this.priceSubscribersTextBox.Text;
            Properties.Settings.Default.FAQ = this.richTextBox2.Text;
            Properties.Settings.Default.Save();
            this.Channel = this.channelTextBox.Text;
        }

        private async void SendMessageToAllUsers_Click(object sender, EventArgs e)
        {
            var text = this.richTextBox1.Text;
            this.label4.Text = "0";
            await Task.Run(async () =>
            {
                foreach (var item in this.users)
                {
                    try
                    {
                        await this.client.SendTextMessageAsync(item.ID, text);
                        this.BeginInvoke(new Action(() => { this.label4.Text = (Convert.ToInt32(this.label4.Text) + 1).ToString(); }));
                        Thread.Sleep(5000);
                    }
                    catch
                    {

                    }
                }
            });
        }
    }
}
