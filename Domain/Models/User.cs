using Domain.Enum;

namespace Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public Guid User_id { get; set; }
    
    public string Login { get; set; }
    
    public Role Role { get; set; }
    public string Email { get; set; }
    
    public string Password { get; set; }
    
    public DateTime CreatedAt { get; set; }
}