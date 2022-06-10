using System.Data;
using Covid_App.Entities;

namespace Covid_App.Services.Data
{
    public interface IDataService
    {
        Dictionary<int, double> GetBalanceOfServices(string filepath);
        SortedDictionary<DateTime, int> GetDeathsData();
        Dictionary<string, int> GetDeathsCountBeforeCovid();
        Dictionary<string, int> GetDeathsCountWhileCovid();
        Dictionary<string, Int32> GetBlikPayments();
        Dictionary<string, Int32> GetFluData();
        void ExportXmlFile();
        void ExportJsonFile(string path);
        void ExportBlikJsonFile(string path);
        void ExportFluJsonFile(string path);
    }
}
