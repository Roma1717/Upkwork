using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;

namespace Domain.ModelsDb;
[Table("users")]
public class UserDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("login")]
    public string Login { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("password")]
    public string Password { get; set; }
    
    [Column("role")]
    public Role Role { get; set; }
    [Column("user_id")]
    public Guid User_id { get; set; }
    
    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }
    
}