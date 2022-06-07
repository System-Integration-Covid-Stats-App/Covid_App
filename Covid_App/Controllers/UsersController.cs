using Covid_App.Entities;
using Microsoft.AspNetCore.Mvc;
using Covid_App.Model;
using Covid_App.Model.Request;
using Covid_App.Model.Response;
using Covid_App.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

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

    [HttpGet("user/{userId}")]
    [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> GetUserById(int userId)
    {
        var response = _userService.findUserById(userId);
        return response;
    }
    [HttpPatch("updateAccount/{userId}")]
    [Authorize(Roles = "user", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> UpdateUser(int userId,UpdateUserRequest request)
    {
        var response = _userService.UpdateUser(userId,request);
        return Ok(response);
    }
    
    [HttpDelete("deleteAccount/{userId}")]
    [Authorize(Roles = "user,admin", AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public ActionResult<User> DeleteUser(int userId)
    {
        var response = _userService.DeleteUser(userId);
        return Ok(response);
    }

}

