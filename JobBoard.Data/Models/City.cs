using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class City : BaseEntity
{
    [Required]
    public int StateId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual State State{ get; set; } = null!;

    public virtual ICollection<Users> Users{ get; set; } = [];
}
