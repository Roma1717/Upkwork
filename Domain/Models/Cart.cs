namespace Domain.Models;

public class Cart
{
     public Guid Id { get; set; }
     public Guid UserId { get; set; }
     public Guid JobId { get; set; }

    public DateTime AddedAt { get; set; }
}