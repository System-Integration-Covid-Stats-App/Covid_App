using Covid_App.Entities;
using Microsoft.AspNetCore.Mvc;
using Covid_App.Model;
using Covid_App.Model.Request;
using Covid_App.Model.Response;
using Covid_App.Services.Users;

namespace Covid_App.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IAuthService _authService;
    
    public UsersController(IUserService userService,IAuthService authService)
    {
        _userService = userService;
        _authService = authService;
    }
    
    
    [HttpPost("authenticate")]
    public ActionResult<AuthenticationResponse>Authenticate(AuthenticationRequest request)
    {
        var response = _authService.Authenticate(request);
        if (response == null)
            return BadRequest(new
            {
                message = "Username or password is incorrect" 
            });
        return Ok(response);
    }
    [HttpPost("createAccount")]
    public ActionResult<CreateUserResponse> CreateUser(UserRequest request)
    {
        var response = _userService.RegisterUser(request);
        return Ok(response);
    }

    [HttpPatch("updateAccount/{userId}")]
    public ActionResult<User> UpdateUser(int userId,UserRequest request)
    {
        var response = _userService.UpdateUser(userId,request);
        return Ok(response);
    }

}

