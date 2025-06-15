using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Net.NetworkInformation;

namespace JobBoard.Data.Models;

public class Company : BaseEntity
{   
    public int UserId { get; set;}  

    [StringLength(100)]
    public string Name { get; set;} = null!;

    public string? Logo { get; set;}
    
    public string Details { get; set;} = null!;

    public virtual Users? User{ get; set;}
}
