using System.ComponentModel.DataAnnotations;
using JobBoard.Data.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Data.ViewModels;

public class RegisterEmployer
{   
    
    [Required(ErrorMessage = ErrorMessageConstant.CityRequired)]
    public int CityId { get; set;}
    
    [Required(ErrorMessage = ErrorMessageConstant.StateRequired)]
    public int StateId { get; set;}
    
    [Required(ErrorMessage = ErrorMessageConstant.CityRequired)]
    public int CountryId { get; set;}
    
    [Required(ErrorMessage = ErrorMessageConstant.EmailRequired)]
    public string Email { get; set;} = null!;
    
    [Required(ErrorMessage = ErrorMessageConstant.PasswordRequired)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$",//{4,}$" issue causing 4 is min lenght
         ErrorMessage = ErrorMessageConstant.PasswordSpecialChar)]
    [StringLength(18, ErrorMessage = ErrorMessageConstant.PasswordLenght, MinimumLength = 12)]
    public string Password { get; set;} = null!;

    [Required(ErrorMessage = ErrorMessageConstant.FNameRequired)]
    [StringLength(50, MinimumLength =3, ErrorMessage = ErrorMessageConstant.FNameValid)]
    public string FName { get; set;} = null!;
    
    [Required(ErrorMessage = ErrorMessageConstant.LNameRequired)]
    [StringLength(50, MinimumLength =3, ErrorMessage = ErrorMessageConstant.LNameValid)]
    public string LName { get; set;} = null!;
    
    [Required(ErrorMessage = ErrorMessageConstant.AddressRequired)]
    [StringLength(255)]
    public string Address { get; set;} = null!;
    
    [Required(ErrorMessage = ErrorMessageConstant.ZipCodeRequired)]
    [Range(100000,999999,ErrorMessage = ErrorMessageConstant.ZipCodeValid)]
    public int ZipCode { get; set;} 

    public IFormFile? Profile { get; set;}

    public string? ProfileImg {get;set;}

    [Required(ErrorMessage =ErrorMessageConstant.CompanyNameRequired)]
    public string CompanyName { get; set;} = null!;

    public IFormFile? CompanyLogo { get; set;}
    public string? LogoPath {get;set;}
    
    [Required(ErrorMessage = ErrorMessageConstant.CompanyDetailRequired)]
    public string CompanyDetails { get; set;} = null!;
}
