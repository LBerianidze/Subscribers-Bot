using System;

namespace Telegram_Subscribers_Cheater_Bot17._09._2018
{
    [Serializable]
    public class BotUser
    {
        public long ID { get; set; }
        public int LastMessage { get; set; }
        public Mode Mode { get; set; } = Mode.None;
        public Step Step { get; set; } = Step.Zero;
        public Order Order;
    }
    public enum Mode
    {
        None,
        AutoView,
        Subscribers,
        TestAccess,
        MyOrders,
        ContactWithSupport,
        FAQ
    }
    public enum Step
    {
        Zero, One, Two, Three, Four, Five, Six
    }
    [Serializable]
    public class Order : ICloneable
    {
        public string[] Answers { get; set; }
        public long ID { get; set; }
        public long UserID { get; set; }
        public Mode Mode { get; set; }
        public float MoneyCount { get; set; }
        public DateTime ActivateDate { get; set; }
        public DateTime AddDate { get; set; }
        public bool Notified { get; set; }
        public object Clone()
        {
            Order ord = new Order
            {
                Answers = new string[Answers.Length]
            };
            for (int i = 0; i < Answers.Length; i++)
            {
                ord.Answers[i] = Answers[i];
            }
            ord.ID = ID;
            ord.UserID = UserID;
            ord.Mode = Mode;
            ord.MoneyCount = MoneyCount;
            ord.ActivateDate = new DateTime(this.ActivateDate.Year, this.ActivateDate.Month,
                              this.ActivateDate.Day,this.ActivateDate.Hour, this.ActivateDate.Minute,0);
            ord.AddDate = new DateTime(this.AddDate.Year, this.AddDate.Month,
                             this.AddDate.Day, this.AddDate.Hour, this.AddDate.Minute, 0);
            return ord;
        }
    }
}
