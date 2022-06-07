using System.ComponentModel.DataAnnotations;

namespace Covid_App.Model.Request;

public class UpdateUserRequest
{
    [Required]
    public string? Username { get; set; }
    [Required]
    public string? Email { get; set; }
}