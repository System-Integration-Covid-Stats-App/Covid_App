using Microsoft.AspNetCore.Mvc;
using Covid_App.Model;
using Covid_App.Services.Users;

namespace Covid_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }
    
    
    [HttpPost("authenticate")]
    public ActionResult<AuthenticationResponse>Authenticate(AuthenticationRequest request)
    {
        Console.Write(request.Username);
        var response = _userService.Authenticate(request);
        if (response == null)
            return BadRequest(new
            {
                message = "Username or password is incorrect" 
            });
        return Ok(response);
    }
   
}

