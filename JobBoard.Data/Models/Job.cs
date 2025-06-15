using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace JobBoard.Data.Models;

public class Job : BaseEntity
{
    public int EmployerId { get; set; }

    public int CategoryId { get; set; }

    public int CountryId { get; set; }

    public int StateId { get; set; }

    public int CityId { get; set; }

    [StringLength(100)]
    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;
    
    public int Salary { get; set; }

    public int MinExperience { get; set; }

    [DefaultValue(false)]
    public bool Status { get; set; }

    public DateTime Deadline { get; set; }

    public virtual Users Employer{ get; set; } = null!;

    public virtual Country Country{ get; set; } =  null!;

    public virtual JobCategory Category{ get; set; } = null!;

    public virtual State State  { get; set; } = null!;

    public virtual City City{ get; set; } = null!;

    public virtual ICollection<JobApplicants> Applicants { get; set; } = [];
}
