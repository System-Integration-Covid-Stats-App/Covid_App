using Covid_App.Data;
using Covid_App.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Covid_App.Services.Admin;

public class AdminServiceImpl : IAdminService
{
    private readonly DataContext _dbContext;
    private readonly IConfiguration _configuration;
    
    public AdminServiceImpl(IConfiguration configuration, DataContext dbContext)
    {
        _configuration = configuration;
        _dbContext = dbContext;
    }
    public ActionResult<List<User>> getAllUsers()
    {
        List<User> users = _dbContext.Users.ToList();
        users.RemoveAt(0);
        return users;
    }
}