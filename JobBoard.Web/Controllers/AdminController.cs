using JobBoard.Data.ViewModels;
using JobBoard.Services.Interfaces;
using JobBoard.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Web.Attributes.CustomAuthorize;

namespace JobBoard.Web.Controllers;

[CustomAuthorize(UserRoles.Admin)]
public class AdminController : Controller
{
    private readonly IUserServices _userServices;

    public AdminController(IUserServices userServices)
    {
        _userServices = userServices;
    }

    public IActionResult Index()
    {
        List<AdminEmployerView> data = _userServices.GetUnapprovedEmployer().Result;
        return View(data);
    }

    public IActionResult ApproveEmployer(int employerId)
    {
        string Message =  _userServices.ApproveEmployer(employerId).Result;
        return Ok(new { success = true, message = Message });
    }

}
