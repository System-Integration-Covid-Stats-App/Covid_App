using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace Covid_App.Entities;


public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    
    [JsonIgnore]
    public string Password { get; set; } = null!;
    
    public string? Email { get; set; }
    
    [JsonIgnore]
    public IList<UserRole> UserRoles { get; set; } = null!;
}

