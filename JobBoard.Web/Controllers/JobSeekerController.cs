using JobBoard.Services.Interfaces;
using JobBoard.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Web.Attributes.CustomAuthorize;

namespace JobBoard.Web.Controllers;

[CustomAuthorize(UserRoles.JobSeeker)]
public class JobSeekerController : Controller
{

    private readonly IJobServices _jobServices;

    public JobSeekerController(IJobServices jobServices)
    {
        _jobServices = jobServices;
    }

    public IActionResult Index()
    {
        
        return View();
    }
}
