namespace JobBoard.Web.Constants;

public static class UserRoles
{
    public const string Admin = "Admin";

    public const string Employer = "Employer";

    public const string JobSeeker = "JobSeeker";

    public static readonly List<string> All = ["Admin", "Employer", "JobSeeker"];

}
