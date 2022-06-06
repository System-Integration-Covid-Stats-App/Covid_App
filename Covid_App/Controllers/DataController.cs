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
        //[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Dictionary<int, double>> GetBalanceOfServices()
        {
            string xmlpath = Path.Combine("Assets", "data.xml");
            var balanceOfServices = dataService.GetBalanceOfServices(xmlpath);
            return Ok(balanceOfServices);
        }

        [HttpGet("deathsBeforeCovid")]
        //[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Dictionary<string, int>> GetDeathsCountBeforeCovid()
        {
            var deaths = dataService.GetDeathsCountBeforeCovid();
            return Ok(deaths);
        }
        
        [HttpGet("deathsWhileCovid")]
        //[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Dictionary<string, int>> GetDeathsCountWhileCovid()
        {
            var deaths = dataService.GetDeathsCountWhileCovid();
            return Ok(deaths);
        }

        [HttpGet("blik")]
        //[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Dictionary<string, Int32>> GetBlikPayments()
        {
            var blik = dataService.GetBlikPayments();
            return Ok(blik);
        }

        [HttpGet("flu")]
        //[Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public ActionResult<Dictionary<string, Int32>> GetFluData()
        {
            var flu = dataService.GetFluData();
            return Ok(flu);
        }
    }
}
