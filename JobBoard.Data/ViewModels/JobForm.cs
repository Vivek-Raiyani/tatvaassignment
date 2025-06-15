using System.ComponentModel.DataAnnotations;
using JobBoard.Data.Constants;

namespace JobBoard.Data.ViewModels;

public class JobForm
{   
    public int Id { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.CategoryRequired)]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.CountyRequired)]
    public int CountryId { get; set; }

    [Required(ErrorMessage =ErrorMessageConstant.StateRequired)]
    public int StateId { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.CityRequired)]
    public int CityId { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.JobTitleRequired)]
    [StringLength(100)]
    public string Title { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.JobDescriptionRequired)]
    public string Description { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.SalaryReqired)]
    [Range(1,int.MaxValue,ErrorMessage = ErrorMessageConstant.SalaryValidate)]
    public int Salary { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.MinExpRequired)]
    public int MinExperience { get; set; } = -1;

    [Required]
    public DateTime Deadline { get; set; }

    public string? CompanyName { get; set; }

    public string? CompanyLogo { get; set; }

    public int EmployerId { get; set; }
}
