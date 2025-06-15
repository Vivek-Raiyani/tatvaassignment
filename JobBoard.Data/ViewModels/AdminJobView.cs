namespace JobBoard.Data.ViewModels;

public class AdminJobView
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string? CompanyName { get; set; }

    public string? CompanyLogo { get; set; }

    public string EmployerName { get; set; } = null!;
}
