using System.Globalization;
using Covid_App.Entities;
using Newtonsoft.Json;
using System.Xml;

namespace Covid_App.Services.Data
{
    public class DataService : IDataService
    {
        public Dictionary<int, double> GetBalanceOfServices(string filepath)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filepath);

            int year;
            string name;
            string balance;
            var balanceOfServices = new Dictionary<int, double>();
            var section = doc.GetElementsByTagName("podzial");
            XmlNodeList years;
            XmlNode amount;
            foreach (XmlNode s in section)
            {
                name = s.Attributes.GetNamedItem("nazwa").Value;
                if (name == "saldo usług")
                {
                    years = s.FirstChild.ChildNodes;

                    for (int i = 0; i < years.Count; i++)
                    {
                        year = Int16.Parse(years[i].Attributes.GetNamedItem("rok").Value);
                        amount = years[i].FirstChild;
                        if (year >= 2018 && year <= 2021)
                        {
                            balance = amount.InnerText.Trim('"');
                            balanceOfServices.TryAdd(year, Convert.ToDouble(balance));
                        }
                    }
                }
            }

            return balanceOfServices;
        }

        public Dictionary<string, int> GetDeathsCount()
        {
            string path = Directory.GetCurrentDirectory();
            StreamReader r = File.OpenText(path + "/Assets/data.json");
            string json = r.ReadToEnd();
            List<JsonData> items = JsonConvert.DeserializeObject<List<JsonData>>(json);
            //var nullCount = 0;
            SortedDictionary<DateTime, int> monthlyStats = new SortedDictionary<DateTime, int>();
            Dictionary<string, int> monthlyStatsString = new Dictionary<string, int>();
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Kod_ter == null)
                {
                    items.Remove(items[i]);
                    //nullCount++;
                    i--;
                }
            }
            
            foreach (var i in items)
            {
                string[] formats = {"MM yyyy"};
                if (Convert.ToInt16(i.Rok) >= 2018)
                {
                    var date = i.Miesiac + " " + i.Rok;
                    var dateTime = DateTime.ParseExact(date, formats, null, DateTimeStyles.None);
                    if (!monthlyStats.ContainsKey(dateTime))
                    {
                        monthlyStats.Add(dateTime, Convert.ToInt32(i.Liczba_zgonow));
                    }
                    else
                    {
                        monthlyStats[dateTime] += Convert.ToInt32(i.Liczba_zgonow);
                    }
                }
            }

            foreach (var m in monthlyStats)
            {
                monthlyStatsString.Add(m.Key.ToString("MM yyyy"), m.Value);
            }
            
            return monthlyStatsString;
        }

        public List<BlikPayments> GetBlikPayments()
        {
            string path = Directory.GetCurrentDirectory();
            StreamReader r = File.OpenText(path + "/Assets/dataBlik.json");
            string json = r.ReadToEnd();
            List<BlikPayments> items = JsonConvert.DeserializeObject<List<BlikPayments>>(json);
            return items;
        }

        public List<FluData> GetFluData()
        {
            string path = Directory.GetCurrentDirectory();
            StreamReader r = File.OpenText(path + "/Assets/dataFlu.json");
            string json = r.ReadToEnd();
            List<FluData> items = JsonConvert.DeserializeObject<List<FluData>>(json);
            return items;
        }
    }
}