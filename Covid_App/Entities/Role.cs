using System.ComponentModel.DataAnnotations;

namespace Covid_App.Entities;
public class Role
{
    [Key]public int RoleId { get; set; }
    public string Name { get; set; } = null!;

    public IList<UserRole>? UserRoles { get; set; }
}


