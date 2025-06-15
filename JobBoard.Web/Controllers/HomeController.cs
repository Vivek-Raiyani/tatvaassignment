using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using JobBoard.Web.Models;
using JobBoard.Services.Interfaces;
using JobBoard.Data.Models;
using JobBoard.Services.Utils;
using System.Security.Claims;

namespace JobBoard.Web.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUserServices _userServices;
    private readonly IJwtServices _jwtServices;

    public HomeController(ILogger<HomeController> logger, IJwtServices jwtServices, IUserServices userServices)
    {
        _logger = logger;
        _userServices = userServices;
        _jwtServices = jwtServices;
    }

    public IActionResult Index()
    {
        if (!string.IsNullOrEmpty(Request.Cookies["UserData"]))
        {
            Users? user = _userServices.GetUser(Request.Cookies["UserData"] ?? "");
            SessionUtils.SetUser(HttpContext, user);

            string token = _jwtServices.GenerateJwtToken(user.Email, user.Role.Name ?? "");
            CookieUtils.SaveJWTToken(Response, token);

            switch (user.RoleId)
            {
                case 1:
                    return RedirectToAction("Index", "Employer");
                case 2:
                    return RedirectToAction("Index", "Employer");
                default:
                    return RedirectToAction("Index", "JobSeeker");
            }
        }
        else if (!string.IsNullOrEmpty(Request.Cookies["SuperSecretAuthToken"]))
        {
            ClaimsPrincipal? principal = _jwtServices.ValidateToken(Request.Cookies["SuperSecretAuthToken"]);
            string? email = principal?.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            Users? user = _userServices.GetUser(email);
            SessionUtils.SetUser(HttpContext, user);

            switch (user.RoleId)
            {
                case 1:
                    return RedirectToAction("Index", "Admin");
                case 2:
                    return RedirectToAction("Index", "Employer");
                default:
                    return RedirectToAction("Index", "JobSeeker");
            }
        }

        CookieUtils.ClearCookies(HttpContext);
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

}
