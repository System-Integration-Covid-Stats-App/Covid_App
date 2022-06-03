using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Covid_App.Data;
using Covid_App.Entities;
using Covid_App.Model;
using Covid_App.Model.Request;
using Covid_App.Model.Response;
using Microsoft.IdentityModel.Tokens;

namespace Covid_App.Services.Users;

public class UserServiceImpl : IUserService
{
    private readonly DataContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public UserServiceImpl(IConfiguration configuration, DataContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }
    
    public CreateUserResponse RegisterUser(UserRequest request)
    {
        User user = CreateUser(request);
        UserRole userRole = CreateUserRole(user.UserId);
        CreateUserResponse createUserResponse = new CreateUserResponse
        {
            UserId = user.UserId,
            Username = user.Username,
        };
        return createUserResponse;
    }

    public User UpdateUser(int userId, UserRequest request)
    {
        User user = findUserById(userId);
        user.Username = request.Username;
        user.Password = request.Password;
        _dbContext.SaveChanges();
        return user;
    }


    private User findUserById(int userId)
    {
        var user = _dbContext.Users.Find(userId);
        return user;
    }
    private User CreateUser(UserRequest request)
    {
        var user = new User
        {
            Username = request.Username,
            Password = request.Password,
        };
        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
        return user;
    }

    private UserRole CreateUserRole(int userId)
    {
        var userRole = new UserRole
        {
            RoleId = 2,
            UserId = userId
        };
        _dbContext.UserRoles.Add(userRole);
        _dbContext.SaveChanges();
        return userRole;
    }

}


