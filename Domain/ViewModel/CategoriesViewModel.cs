namespace Domain.ViewModel;

public class CategoriesViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    
    public Guid UserId { get; set; }

    public string PathImg { get; set; }
    
    public string Description { get; set; }
    
    public DateTime CreatedAt { get; set; }

}