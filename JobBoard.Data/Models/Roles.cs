using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobBoard.Data.Models;

public class Roles : BaseEntity
{
    [StringLength(10)]
    public string Name { get; set;} = null!;

    [JsonIgnore]
    public virtual ICollection<Users> Users { get; set;} = [];
}
