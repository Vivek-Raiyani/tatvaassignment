using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class JobSeeker : BaseEntity
{
    public int UserId { get; set; }

    public DateOnly DOB { get; set; }

    // validation
    [StringLength(maximumLength:12, MinimumLength =10)]
    public string Phone { get; set; } = null!;

    public string? Resume { get; set; }

    public virtual Users User { get; set; } = null!;

}
