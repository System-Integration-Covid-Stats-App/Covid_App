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

        public List<JsonData> GetDeathsCount()
        {
            string path = Directory.GetCurrentDirectory();
            StreamReader r = File.OpenText(path + "/Assets/data.json");
            string json = r.ReadToEnd();
            List<JsonData> items = JsonConvert.DeserializeObject<List<JsonData>>(json);
            //var nullCount = 0;
            foreach (var i in items)
            {
                Console.WriteLine(i);
            }
            
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].Kod_ter == null)
                {
                    items.Remove(items[i]);
                    //nullCount++;
                    i--;
                }
            }
            return items;
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