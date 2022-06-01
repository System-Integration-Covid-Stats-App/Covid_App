using System.ComponentModel.DataAnnotations;

namespace Covid_App.Model.Request;

public class CreateUserRequest
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Password { get; set; }
}