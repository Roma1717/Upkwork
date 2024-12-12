using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.ModelsDb;

[Table("jobs")]
public class JobsDb
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("employer_id")]
    public Guid Employer_id { get; set; }

    [Column("title")]
    public string Title { get; set; }

    [Column("description")]
    public string Description { get; set; }
    
    [Column("requirements")]
    public string Requirements { get; set; }
    
    [Column("salary")]
    public int salary { get; set; }
    
    [Column("location")]
    public string Location { get; set; }
    
    [Column("category_id")]
    public Guid Category_id { get; set; }
 
    [Column("is_active")]
    public bool Is_active { get; set; }
    

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }   
}