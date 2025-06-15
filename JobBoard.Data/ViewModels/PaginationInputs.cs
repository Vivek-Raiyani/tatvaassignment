namespace JobBoard.Data.ViewModels;

public class PaginationInputs
{
    public int Page { get; set;} = 1;

    public int PageSize { get; set;} = 5;

    public string? Search { get; set;}

    public int CategoryId { get; set;}

    public int CountryId { get; set;}  
}
