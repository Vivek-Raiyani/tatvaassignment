using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class JobCategory : BaseEntity
{   
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<Job> Jobs { get; set;} = [];
}
