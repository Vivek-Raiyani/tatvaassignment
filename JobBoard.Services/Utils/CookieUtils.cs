using Microsoft.AspNetCore.Http;

namespace JobBoard.Services.Utils;

public static class CookieUtils
{
    public static void SaveJWTToken(HttpResponse response, string token)
    {
        response.Cookies.Append("SuperSecretAuthToken", token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            Expires = DateTime.UtcNow.AddMinutes(300)
        });
    }

    public static string? GetJWTToken(HttpRequest request)
    {
        _ = request.Cookies.TryGetValue("SuperSecretAuthToken", out string? token);
        return token;
    }

    public static void SaveUserData(HttpResponse response, string email)
    {
        CookieOptions cookieOptions = new()
        {
            Expires = DateTime.UtcNow.AddDays(3),
            HttpOnly = true,
            Secure = true,
            IsEssential = true
        };
        response.Cookies.Append("UserData", email, cookieOptions);
    }

    public static void ClearCookies(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete("SuperSecretAuthToken");
        httpContext.Response.Cookies.Delete("UserData");
    }

}
