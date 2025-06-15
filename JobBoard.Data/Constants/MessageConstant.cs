using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace JobBoard.Data.Constants;

public static class MessageConstant
{
    public const string LoginSucess = "Login Successfull!";

    public const string LoginError = "Invalid Credentials!";

    public const string LogoutSuccess = "Logout Successfull!";

    public const string RegisterSuccess = "Registration was Successfull!";

    public const string RegistrationError = "Registration Failed!";
}
