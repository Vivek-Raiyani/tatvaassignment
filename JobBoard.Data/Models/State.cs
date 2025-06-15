using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class State : BaseEntity
{
    [Required]
    public int CountryId { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; } = null!;

    public virtual Country? Country{ get; set; }

    public virtual ICollection<City> Cities{ get; set; } = [];

    public virtual ICollection<Users> Users{ get; set; } = [];
}
