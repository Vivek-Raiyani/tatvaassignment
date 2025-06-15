using JobBoard.Data.Models;
using JobBoard.Data.ViewModels;
using JobBoard.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace JobBoard.Web.Controllers;

public class BaseController : Controller
{
    private readonly IGenericServices<Country> _countryService;
    private readonly IJobServices _jobServices;
    private readonly IGenericServices<State> _stateService;
    private readonly IGenericServices<City> _cityService;

    private readonly IGenericServices<JobCategory> _jobCategoryService;

    public BaseController(IGenericServices<Country> countryService, IGenericServices<State> stateService, IGenericServices<City> cityService,
                            IGenericServices<JobCategory> jobCategoryServices, IJobServices jobServices)
    {
        _countryService = countryService;
        _stateService = stateService;
        _cityService = cityService;
        _jobCategoryService = jobCategoryServices;
        _jobServices = jobServices;
    }

    public IActionResult GetCountries()
    {   
        List<Country> countries = _countryService.GetAllAsync().Result.ToList();
        
        if(countries.Count != 0)
        {
            return Ok(new { succes = true, message = "Country Fetched Successfully", data = countries });
        }

        return Ok(new { succes = false, message = "Error occured while fetching country data"});
    }

    public IActionResult GetStates(int countryId)
    {   
        List<State> states = _stateService.GetAllWithFilterAsync(c=> c.CountryId == countryId).Result.ToList();
        
        if(states.Count != 0)
        {
            return Ok(new { succes = true, message = "States Fetched Successfully", data = states });
        }
        
        return Ok(new { succes = false, message = "Error occured while fetching country data"});
    }

    public IActionResult GetCities(int stateId)
    {   
        List<City> cities = _cityService.GetAllWithFilterAsync(c=> c.StateId == stateId).Result.ToList();
        
        if(cities.Count != 0)
        {
            return Ok(new { succes = true, message = "Cities Fetched Successfully", data = cities });
        }
        
        return Ok(new { succes = false, message = "Error occured while fetching country data"});
    }

    public IActionResult GetJobCategories()
    {
        List<JobCategory> categories = _jobCategoryService.GetAllAsync().Result.ToList();
        
        if(categories.Count != 0)
        {
            return Ok(new { succes = true, message = "Categories Fetched Successfully", data = categories });
        }

        return Ok(new { succes = false, message = "Error occured while fetching categories data"});
    }

    public IActionResult PaginatedList(PaginationInputs inputs)
    {
        var data = _jobServices.GetPaginatedJobs(inputs).Result;
        return Ok(data);
    }

}
