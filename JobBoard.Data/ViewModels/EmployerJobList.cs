namespace JobBoard.Data.ViewModels;

public class EmployerJobList
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public string Category { get; set; } = null!;
    public int Applicants { get; set; }

    public DateTime Deadline { get; set; }
    public bool Status { get; set; }
}
