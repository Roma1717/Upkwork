using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.ModelsDb;

[Table("categories")]
public class CategoriesDb
{
    [Column("id")]
    public Guid Id { get; set; }
    
    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("name")]
    public string Name { get; set; }

    [Column("description")]
    public string Description { get; set; }
    
    [Column("path_img")]
    public string PathImg { get; set; }
    
    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; } 
}