using System.Text.Json.Serialization;
using Covid_App.Entities;

namespace Covid_App.Model.Response;

public class CreateUserResponse
{
    public int UserId { get; set; }
    public string Username { get; set; } = null!;
    
    
    
}