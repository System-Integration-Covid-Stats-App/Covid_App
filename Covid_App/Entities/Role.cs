using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Covid_App.Entities;
public class Role
{
    
    [Key]public int RoleId { get; set; }
    
    public string Name { get; set; } = null!;

    public IList<UserRole>? UserRoles { get; set; }
}


