using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.ModelsDb;

[Table("applications")]
public class ApplicationsDb
{
    [Column("id")]
    public Guid Id { get; set; }

    [Column("job_id")]
    public Guid Job_id { get; set; }

    [Column("jobseeker_id")]
    public Guid Jobseeker_id { get; set; }

    [Column("status")]
    public string Status { get; set; }

    [Column("created_at", TypeName = "timestamp")]
    public DateTime CreatedAt { get; set; }
}