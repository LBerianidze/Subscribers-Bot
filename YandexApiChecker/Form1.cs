using LBYandexMoneyApi;
using System;
using System.Linq;
using System.Windows.Forms;

namespace YandexApiChecker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public YandexMoneyApi YandexApi { get; private set; }

        private async void button1_Click(object sender, EventArgs e)
        {
            YandexApi = new LBYandexMoneyApi.YandexMoneyApi(textBox1.Text, 10000, LBYandexMoneyApi.HistoryType.IN);
            Yandex.Money.Api.Sdk.Responses.OperationHistoryResult history = await YandexApi.GetHistory();
            MessageBox.Show(string.Join("-", history.Operations.Select(t=>t.Message)));
            YandexApi.Start();
        }
    }
}