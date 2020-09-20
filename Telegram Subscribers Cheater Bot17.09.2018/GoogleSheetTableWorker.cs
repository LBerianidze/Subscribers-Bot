using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Google.Apis.Sheets.v4.SheetsService;

namespace Telegram_Subscribers_Cheater_Bot17
{
    internal class GoogleSheetTableWorker
    {
        private static string[] Scopes = { Scope.Spreadsheets };
        private static string ApplicationName = "TelegramBot";
        private SheetsService SheetsServiceE;
        private readonly string _sheetid;
        private List<Sheet> Sheets;
        public GoogleSheetTableWorker(string SheetID)
        {
            this._sheetid = SheetID;
            UserCredential credential;
            using (FileStream stream = new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = System.Environment.GetFolderPath(
                    System.Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
            }
            SheetsServiceE = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            Sheets = GetSheets().Result.ToList();
        }

        public void UpdateTable(int table,params string[] values)
        {
            string spreadsheetId = _sheetid;
            string range2 = $"{Sheets[table].Name}!A{Sheets[table].Count}:J{Sheets[table].Count}";
            Sheets[table].Count++;
            ValueRange valueRange = new ValueRange
            {
                MajorDimension = "ROWS"
            };
            valueRange.Values = new List<IList<object>>
            {
                values
            };
            SpreadsheetsResource.ValuesResource.UpdateRequest update = SheetsServiceE.Spreadsheets.Values.Update(valueRange, spreadsheetId, range2);
            update.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;
            UpdateValuesResponse result2 = update.Execute();
        }
        public async Task<IEnumerable<Sheet>> GetSheets()
        {
            Spreadsheet spreadsheets = SheetsServiceE.Spreadsheets.Get(_sheetid).Execute();
            string[] titles = spreadsheets.Sheets.Select(s => s.Properties.Title).ToArray();
            List<Sheet> shl = new List<Sheet>();
            for (int i = 0; i < titles.Length; i++)
            {
                Sheet sh = new Sheet
                {
                    Name = titles[i]
                };
                SpreadsheetsResource.ValuesResource.GetRequest request =
                        SheetsServiceE.Spreadsheets.Values.Get(_sheetid, titles[i] + "!A1:B");
                var values = request.Execute();
                if(values.Values!=null)
                sh.Count = values.Values.Count + 1;
                shl.Add(sh);
            }
            return shl;
        }
    }
    public class Sheet
    {
        public string Name { get; set; }
        public int Count { get; set; } = 1;
    }
}