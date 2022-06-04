using Covid_App.Entities;

namespace Covid_App.Services.Data
{
    public interface IDataService
    {
        Dictionary<int, double> GetBalanceOfServices(string filepath);
        List<JsonData> GetDeathsCount();
        List<BlikPayments> GetBlikPayments();
        List<FluData> GetFluData();
    }
}
