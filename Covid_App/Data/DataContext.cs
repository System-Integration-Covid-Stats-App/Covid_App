using Covid_App.Entities;
using Covid_App.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Covid_App.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    { 
       // base.OnModelCreating(builder);
        builder.Entity<UserRole>().HasKey(sc => new {sc.RoleId,sc.UserId});
        SeedUser(builder);
    }
    private static void SeedUser(ModelBuilder builder)
    {
        var roleAdmin = CreateRole(1,"admin");
        var roleUser = CreateRole(2,"user");
        
        var user1 = CreateUser(1, "Andrzej", "Andrzej");
        var user2 = CreateUser(2, "Pawel", "Pawel");
        var user3 = CreateUser(3, "Andrzej", "Pawel");
        
        
        builder.Entity<Role>().HasData(roleAdmin);
        builder.Entity<Role>().HasData(roleUser);
        
        builder.Entity<User>().HasData(user1);
        builder.Entity<User>().HasData(user2);
        builder.Entity<User>().HasData(user3);

        builder.Entity<UserRole>().HasData(new UserRole
        {
            UserId = 1,
            RoleId = 1,
        });
        builder.Entity<UserRole>().HasData(new UserRole
        {
            UserId = 2,
            RoleId = 2,
        });
        builder.Entity<UserRole>().HasData(new UserRole
        {
            UserId = 3,
            RoleId = 2,
        });
    }
    
    private static Role CreateRole(int id, string name)
    {
        var role = new Role
        {
            RoleId = id,
            Name = name,
        };
        return role;
    }

    private static User CreateUser(int id,string password, string username)
    {
        var user = new User
        {
            UserId = id,
            Password = password,
            Username = username,
        };
        return user;
    }

  
}
