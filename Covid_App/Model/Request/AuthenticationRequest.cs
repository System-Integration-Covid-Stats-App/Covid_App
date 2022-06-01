using System.ComponentModel.DataAnnotations;

namespace Covid_App.Model.Request
{
    public class AuthenticationRequest
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}

