using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Covid_App.Data;
using Covid_App.Entities;
using Covid_App.Model.Request;
using Covid_App.Model.Response;
using Microsoft.IdentityModel.Tokens;

namespace Covid_App.Services.Users;

public class AuthServiceImpl : IAuthService
{
    private readonly DataContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public AuthServiceImpl(IConfiguration configuration, DataContext dbContext)
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

}