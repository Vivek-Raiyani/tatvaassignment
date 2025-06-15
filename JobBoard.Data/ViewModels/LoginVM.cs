using System.ComponentModel.DataAnnotations;
using JobBoard.Data.Constants;

namespace JobBoard.Data.ViewModels;

public class LoginVM
{
    [Required(ErrorMessage = ErrorMessageConstant.EmailRequired)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = ErrorMessageConstant.PasswordRequired)]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{4,}$",
         ErrorMessage = ErrorMessageConstant.PasswordSpecialChar)]
    [StringLength(18, ErrorMessage = ErrorMessageConstant.PasswordLenght, MinimumLength = 12)]
    public string Password { get; set; } = null!;

    public bool RememberMe { get; set; } = false;
}
