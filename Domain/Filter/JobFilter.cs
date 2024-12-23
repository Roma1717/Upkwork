namespace Domain.Filter;

public class JobFilter
{
    public decimal? MinSalary { get; set; }
    public decimal? MaxSalary { get; set; }
    public string SortBy { get; set; } // Поле для сортировки: "Salary", "CreatedAt", и т.д.
    public bool IsDescending { get; set; } // Порядок: true для убывания, false для возрастания
}
