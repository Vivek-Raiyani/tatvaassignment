namespace JobBoard.Data.ViewModels;

public class Paginate<T> where T : class
{
    public List<T> Items { get; set; } = [];
    public int PageIndex { get; set; }

    public int PageSize { get; set;}
    public int TotalPages { get; set;}

    public string Search { get; set;} = "";

    public int TotalItems { get; set;}

}

