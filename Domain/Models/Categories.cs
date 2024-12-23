namespace Domain.Models
{
    public class Categories
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        
        public string PathImg { get; set; }
        
        public Guid UserId { get; set; }

        
        public DateTime CreatedAt { get; set; }

    }
}