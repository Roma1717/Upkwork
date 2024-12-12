namespace Domain.Models
{
    public class Jobs
    {
        public Guid Id { get; set; }

        public Guid Employer_id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Requirements { get; set; }

        public int salary { get; set; }

        public string Location { get; set; }

        public Guid Category_id { get; set; }

        public bool Is_active { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}