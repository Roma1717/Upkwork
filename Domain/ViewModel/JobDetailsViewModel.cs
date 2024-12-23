namespace Domain.ViewModel;

public class JobDetailsViewModel
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Location { get; set; }
    public string Description { get; set; }
    public string Requirements { get; set; }
    public int Salary { get; set; }
    public DateTime CreatedAt { get; set; }
    public Guid CategoryId { get; set; }
}