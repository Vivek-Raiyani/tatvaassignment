using System.ComponentModel.DataAnnotations;
using JobBoard.Data.Constants;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Data.ViewModels;

public class RegisterJobSeaker
{
    [Required(ErrorMessage = ErrorMessageConstant.CityRequired)]
    public int CityId { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.StateRequired)]
    public int StateId { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.CountyRequired)]
    public int CountryId { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.EmailRequired)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.PasswordRequired)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$",//{4,}$" issue causing 4 is min lenght
         ErrorMessage = ErrorMessageConstant.PasswordSpecialChar)]
    [StringLength(18, ErrorMessage = ErrorMessageConstant.PasswordLenght, MinimumLength = 12)]
    public string Password { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.FNameRequired)]
    [StringLength(50,MinimumLength= 3,ErrorMessage =ErrorMessageConstant.FNameValid)]
    public string FName { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.LNameRequired)]
    [StringLength(50,MinimumLength= 3,ErrorMessage =ErrorMessageConstant.LNameValid)]
    public string LName { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.AddressRequired)]
    [StringLength(255)]
    public string Address { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.ZipCodeRequired)]
    [Range(100000, 999999,ErrorMessage = ErrorMessageConstant.ZipCodeValid)]
    public int ZipCode { get; set; }

    public IFormFile? Profile { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.DOBRequired)]
    public DateOnly DOB { get; set; }

    [Required(ErrorMessage = ErrorMessageConstant.PhoneRequired)]
    [StringLength(maximumLength: 12, MinimumLength = 10,ErrorMessage = ErrorMessageConstant.PhoneValid)]
    public string Phone { get; set; } = null!;

    public IFormFile? Resume { get; set; } = null!;

}
