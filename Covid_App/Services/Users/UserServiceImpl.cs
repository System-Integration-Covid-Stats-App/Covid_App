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
    public AuthenticationResponse? Authenticate(AuthenticationRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(b => b.Username == request.Username);
        if (user == null || !user.Password.Equals(request.Password))
        {
            return null;
        }
        var token = GenerateJwtToken(user);
        return new AuthenticationResponse(token);
    }

    public CreateUserResponse RegisterUser(CreateUserRequest request)
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

    public string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Jwt:key"] ?? throw new InvalidOperationException());
        var claims = new List<Claim>();
        claims.Add(new Claim("id", user.UserId.ToString()));
        var userRole = _dbContext.UserRoles.FirstOrDefault(ur => ur.UserId == user.UserId);
        var role = _dbContext.Roles.FirstOrDefault(r => userRole != null && r.RoleId == userRole.RoleId);
        if (role != null) claims.Add(new Claim(ClaimTypes.Role, role.Name));
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new
       ClaimsIdentity(claims.ToArray()),
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    private User CreateUser(CreateUserRequest request)
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


