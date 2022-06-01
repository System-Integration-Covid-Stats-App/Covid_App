using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Covid_App.Entities
{
    public class User
    {
        [Key]public int UserId { get; set; }
        public string Username { get; set; } = null!;

        [JsonIgnore]
        public string Password { get; set; } = null!;

        public IList<UserRole> UserRoles { get; set; } = null!;
    }
}
