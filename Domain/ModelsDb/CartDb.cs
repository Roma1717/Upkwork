using System.ComponentModel.DataAnnotations.Schema;
using Domain.Enum;
namespace Domain.ModelsDb;

[Table("cart")]
public class CartDb
{
    [Column("id")] public Guid Id { get; set; }
    [Column("user_id")] public Guid UserId { get; set; }
    [Column("job_id")] public Guid JobId { get; set; }

    [Column("added_at", TypeName = "timestamp")]
    public DateTime AddedAt { get; set; }
}