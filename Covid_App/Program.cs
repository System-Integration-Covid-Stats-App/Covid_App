using System.Text;
using Covid_App.Data;
using Covid_App.Services.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddCors();
builder.Services.AddControllers();
builder.Services.AddScoped<IUserService,UserServiceImpl>();
builder.Services.AddScoped<IAuthService,AuthServiceImpl>();

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("AppDb") ?? throw new InvalidOperationException());
});


// Authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var key = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("Jwt:Key").Value ?? throw new InvalidOperationException());
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = false,
            ValidateAudience = false,
            // Timezone problem workaround
            LifetimeValidator = (notBefore, expires, _, _) =>
            {
                if (notBefore is not null)
                    return notBefore.Value.AddMinutes(-5) <= DateTime.UtcNow && expires >= DateTime.UtcNow;
                return expires >= DateTime.UtcNow;
            }
        };
    });

var app = builder.Build();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.MapGet("/", () => "Hello World!");

app.Run();