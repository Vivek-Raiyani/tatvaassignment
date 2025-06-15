using System.Text.Json;
using JobBoard.Data.Models;
using Microsoft.AspNetCore.Http;

namespace JobBoard.Services.Utils;

public class SessionUtils
{
    public static void SetUser(HttpContext httpContext, Users user)
    {
        if (user != null)
        {
            string userData = JsonSerializer.Serialize(user);
            httpContext.Session.SetString("UserData", userData);
        }
    }

    public static Users? GetUser(HttpContext httpContext)
    {
        // Check session first
        string? userData = httpContext.Session.GetString("UserData");

        if (string.IsNullOrEmpty(userData))
        {
            // If session is empty, check the cookie
            httpContext.Request.Cookies.TryGetValue("UserData", out userData);
        }
        try
        {
            return string.IsNullOrEmpty(userData) ? null : JsonSerializer.Deserialize<Users>(userData);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }

    }

    public static void ClearSession(HttpContext httpContext) => httpContext.Session.Clear();

}
