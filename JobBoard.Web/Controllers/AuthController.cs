using System.Security.Claims;
using JobBoard.Data.Constants;
using JobBoard.Data.Models;
using JobBoard.Data.ViewModels;
using JobBoard.Services.Interfaces;
using JobBoard.Services.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Web.Controllers;

public class AuthController : Controller
{
    private readonly IGenericServices<Users> _genericUserServices;
    private readonly IUserServices _userServices;
    private readonly IJwtServices _jwtServices;

    private readonly IUploadServices _uploadServices;


    public AuthController(IGenericServices<Users> genericUserServices, IJwtServices jwtServices, IUserServices userServices, IUploadServices uploadServices)
    {
        _genericUserServices = genericUserServices;
        _jwtServices = jwtServices;
        _userServices = userServices;
        _uploadServices = uploadServices;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(LoginVM model)
    {
        if (ModelState.IsValid)
        {
            Users? user = _userServices.GetUser(email: model.Email);

            if (user != null)
            {
                if (!user.IsActive)
                {
                    TempData["error"] = "Please contact Admin!";
                    return View();
                }

                PasswordVerificationResult password_is_correct_1 = PasswordHash.PasswordVerificationResult(model.Password, user);

                if (password_is_correct_1 == PasswordVerificationResult.Success)
                {
                    if (model.RememberMe)
                    {
                        CookieUtils.SaveUserData(HttpContext.Response, user.Email);
                    }

                    string token = _jwtServices.GenerateJwtToken(user.Email, user.Role.Name);

                    if (token == null)
                    {
                        ModelState.AddModelError("Login", "Error occurened in login");

                        return View(user);
                    }

                    CookieUtils.SaveJWTToken(Response, token);

                    SessionUtils.SetUser(HttpContext, user);

                    switch (user.RoleId)
                    {
                        case 1:
                            TempData["success"] = MessageConstant.LoginSucess;
                            return RedirectToAction("Index", "Admin");
                        case 2:
                            TempData["success"] = MessageConstant.LoginSucess;
                            return RedirectToAction("Index", "Employer");
                        default:
                            TempData["success"] = MessageConstant.LoginSucess;
                            return RedirectToAction("Index", "JobSeeker");
                    }
                }
            }
        }
        TempData["error"] = MessageConstant.LoginError;
        return View();
    }

    public IActionResult Register()
    {
        return View();
    }

    public IActionResult RegisterEmployer()
    {
        return View("../Auth/RegisterEmployerPV");
    }

    [HttpPost]
    public IActionResult RegisterEmployer(RegisterEmployer employer)
    {
        string message;

        if (ModelState.IsValid)
        {
            if (employer.Profile != null)
            {
                string path = _uploadServices.Upload(Image: employer.Profile, folder_name: employer.FName + employer.LName);
                employer.ProfileImg = $"{Request.Scheme}://{Request.Host}/{path}";
            }
            if (employer.CompanyLogo != null)
            {
                string path = _uploadServices.Upload(Image: employer.CompanyLogo, folder_name: employer.CompanyName.Replace(" ", ""));
                employer.LogoPath = $"{Request.Scheme}://{Request.Host}/{path}";
            }

            message = _userServices.RegisterEmployer(employer);

            return RedirectToAction("Index", "Auth");
        }

        return View("../Auth/RegisterEmployerPV", employer);
    }

    public IActionResult RegisterJobSeeker()
    {
        return View("../Auth/RegisterJobSeakerPV");
    }

    [HttpPost]
    public IActionResult RegisterJobSeeker(RegisterJobSeaker user)
    {
        string message;

        if (ModelState.IsValid)
        {
            message = _userServices.RegisterJobSeeker(user);
            if(message == null)
            {   
                TempData["sucess"]= MessageConstant.RegisterSuccess;
                return RedirectToAction("Index", "Auth");
            }
            else
            {
                TempData["error"]= message;
                return View("../Auth/RegisterJobSeakerPV", user);
            }
            
        }
        TempData["error"] = MessageConstant.RegistrationError;
        return View("../Auth/RegisterJobSeakerPV", user);
    }

    public IActionResult Logout()
    {
        SessionUtils.ClearSession(HttpContext);
        CookieUtils.ClearCookies(HttpContext);
        TempData["success"] = MessageConstant.LogoutSuccess;
        return RedirectToAction("Index", "Home");
    }

}
