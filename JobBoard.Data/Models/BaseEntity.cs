using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class BaseEntity
{
    [Key]
    public int Id { get; set;}

    [DefaultValue(false)]
    public bool IsDeleted { get; set;}

    public DateTime CreatedAt { get; set;}

    public int CreatedBy { get; set;}

    public DateTime UpdatedAt { get; set;}

    public int UpdatedBy { get; set;}
}
