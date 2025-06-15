using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class Country : BaseEntity
{
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual ICollection<State> States{ get; set; } = [];

    public virtual ICollection<Users> Users{ get; set; } = [];
}
