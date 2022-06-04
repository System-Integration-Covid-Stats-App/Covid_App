using Covid_App.Entities;
using Covid_App.Services.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Covid_App.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private IDataService dataService;
        public DataController(IDataService dataService)
        {
            this.dataService = dataService;
        }

        [HttpGet("stats")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Dictionary<int, double>> GetBalanceOfServices()
        {
            string xmlpath = Path.Combine("Assets", "data.xml");
            var balanceOfServices = dataService.GetBalanceOfServices(xmlpath);
            return Ok(balanceOfServices);
        }

        [HttpGet("json")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<JsonData>> GetDeathsCount()
        {
            var deaths = dataService.GetDeathsCount();
            return Ok(deaths);
        }

        [HttpGet("blik")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<JsonData>> GetBlikPayments()
        {
            var blik = dataService.GetBlikPayments();
            return Ok(blik);
        }

        [HttpGet("flu")]
        [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<List<JsonData>> GetFluData()
        {
            var flu = dataService.GetFluData();
            return Ok(flu);
        }
    }
}
