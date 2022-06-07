using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Covid_App.Data;
using Covid_App.Entities;
using Covid_App.Model;
using Covid_App.Model.Request;
using Covid_App.Model.Response;
using Microsoft.EntityFrameworkCore.Storage;
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
        Console.Write(request.Email);
        Console.Write(request.Password);
        Console.Write(request.Username);
        CreateUserResponse createUserResponse = null;
        using (IDbContextTransaction transaction = _dbContext.Database.BeginTransaction())
        {
            try
            {
                User user = CreateUser(request);
                //test transaction
                //throw new Exception();
                UserRole userRole = CreateUserRole(user.UserId);
                createUserResponse = new CreateUserResponse
                {
                    UserId = user.UserId,
                    Username = user.Username,
                };
                
                transaction.Commit();
            }
            catch(Exception)
            {
                transaction.Rollback();
            }
        }
        return createUserResponse;
    }

    public User GetUserById(int userId)
    {
        throw new NotImplementedException();
    }

    public User UpdateUser(int userId, UpdateUserRequest request)
    {
        User user = findUserById(userId);
        user.Username = request.Username;
        user.Email = request.Email;
        _dbContext.SaveChanges();
        return user;
    }

    public User DeleteUser(int userId)
    {
        User user = findUserById(userId);
        _dbContext.Remove(user);
        _dbContext.SaveChanges();
        return user;
    }

    public User findUserById(int userId)
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
            Email = request.Email
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


