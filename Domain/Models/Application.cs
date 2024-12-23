namespace Domain.Models
{
    public class Application
    {
        public Guid Id { get; set; }

        public Guid Job_id { get; set; }

        public Guid Jobseeker_id { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}