using Covid_App.Entities;
using Covid_App.Services.Admin;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Covid_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AdminController: ControllerBase
{
    private IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("users")]
    [Authorize(Roles = "admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<List<User>> GetAllUser()
    {
        var response = _adminService.getAllUsers();
        return response;
    }
}