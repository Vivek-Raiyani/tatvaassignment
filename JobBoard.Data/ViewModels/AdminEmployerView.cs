namespace JobBoard.Data.ViewModels;

public class AdminEmployerView
{
    public int Id { get; set;}

    public string Name { get; set;} = null!;

    public string Email { get; set;} = null!;

    public string Profile { get; set;} = null!;

    public string CompanyName { get; set;} = null!;

    public string CompanyLogo { get; set;} = null!;
}
