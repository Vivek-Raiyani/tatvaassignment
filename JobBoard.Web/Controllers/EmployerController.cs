using JobBoard.Data.Models;
using JobBoard.Data.ViewModels;
using JobBoard.Services.Interfaces;
using JobBoard.Services.Utils;
using JobBoard.Web.Constants;
using Microsoft.AspNetCore.Mvc;
using static JobBoard.Web.Attributes.CustomAuthorize;

namespace JobBoard.Web.Controllers;

[CustomAuthorize(UserRoles.Employer)]
public class EmployerController : Controller
{
    private readonly IJobServices _jobServices;
    private readonly IUserServices _userServices;

    private readonly IGenericServices<Job> _jobGenericServices;

    public EmployerController(IJobServices jobServices, IGenericServices<Job> jobGenericServices, IUserServices userServices)
    {
        _jobServices = jobServices;
        _jobGenericServices = jobGenericServices;
        _userServices = userServices;
    }

    
    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    // employer page paginations we need to get id from session and the pass it to services
    [HttpPost]
    public IActionResult PaginateList(PaginationInputs inputs)
    {
        Console.WriteLine("----------------------------");
        Console.WriteLine(inputs.ToString());
        Console.WriteLine("---------------------");
        Users? user = SessionUtils.GetUser(HttpContext);
        if (user == null)
        {
            return RedirectToAction("Index", "Home");
        }
        else
        {
            Paginate<EmployerJobList> data = _jobServices.GetJobs(inputs).Result;
            return PartialView("../Employer/JobListPV",data);
        }
    }

    

    public IActionResult AddJob()
    {   
        Users? user = SessionUtils.GetUser(HttpContext);
        if (user == null)
        {
            CookieUtils.ClearCookies(HttpContext);
            TempData["error"] = "Please Login in again";
            return RedirectToAction("Index", "Auth");
        }
        else
        {   
            int employerId = user.Id;
            JobForm data = _jobServices.ReadEmployerAsync(employerId).Result;
            return View("../Employer/AddJobPV", data);
        }
    }

    [HttpPost]
    public IActionResult AddJob(JobForm data)
    {
        if (ModelState.IsValid)
        {
            string message;
            Users? employer = SessionUtils.GetUser(HttpContext);
            if (User != null)
            {
                data.EmployerId = employer.Id;
                message = _jobServices.AddJobAsync(data).Result;
                return RedirectToAction("Index", "Employer");
            }
            else
            {
                message = "Error Occured please try again later";
                return View("../Employer/AddJobPV", data);
            }
        }
        else
        {
            return View("../Employer/AddJobPV", data);
        }
    }

    public IActionResult EditJob(int jobId)
    {
        JobForm data = _jobServices.ReadAsync(jobId).Result;
        ViewData["Header"] = "Edit Job Details";
        return View("../Employer/AddJobPV", data);
    }

    [HttpPost]
    public IActionResult EditJob(JobForm data)
    {
        if (ModelState.IsValid)
        {
            string message;
            Users? employer = SessionUtils.GetUser(HttpContext);
            if (User != null)
            {
                data.EmployerId = employer.Id;
                message = _jobServices.UpdateJobAsync(data).Result;
                return RedirectToAction("Index", "Employer");
            }
            else
            {
                message = "Error Occured please try again later";
                return View("../Employer/AddJobPV", data);
            }
        }
        else
        {
            return View("../Employer/AddJobPV", data);
        }
    }

    public IActionResult JobDetails()
    {
        return View();
    }

    [HttpPost]
    public IActionResult DeleteJob(int jobId)
    {
        string message = _jobServices.DeleteJob(jobId).Result;
        return Ok(message);
    }

}
