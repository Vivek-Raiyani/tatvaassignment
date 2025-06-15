using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace JobBoard.Data.Models;

public class Users : BaseEntity
{
    [Required]
    public int RoleId { get; set;}
    
    [Required]
    public int CityId { get; set;}

    [Required]
    public int StateId { get; set;}

    [Required]
    public int CountryId { get; set;}

    public string Email { get; set;} = null!;

    public string Password { get;set;} = null!;

    [StringLength(50)]
    public string FName { get; set;} = null!;

    [StringLength(50)]
    public string LName { get; set;} = null!;

    public string? Profile { get; set;}

    [StringLength(255)]
    public string Address { get; set;} = null!;

    [Range(100000,999999)]
    public int ZipCode { get; set;} 

    [DefaultValue(true)]
    public bool IsActive { get; set; }

    [DefaultValue(false)]
    public bool TwoFactorAuth { get; set; }

    public virtual Country Country{ get; set;} = null!;

    public virtual State State{ get; set;} = null!;

    public virtual City City{ get; set;} = null!;

    public virtual Roles Role{ get; set;} = null!;

    public virtual ICollection<Company> Companies { get; set;} = [];

    public virtual ICollection<JobSeeker> JobSeakers{ get; set;} = [];

    public virtual ICollection<JobApplicants> Applied { get; set;} = [];

}
