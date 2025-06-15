namespace JobBoard.Data.ViewModels;

public class LandingJobView
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Country { get; set; } = null!;

    public string State { get; set; } = null!;

    public string City { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? CompanyLogo { get; set; } 

    public DateTime PostedAt { get; set; } 

}
