using System.Linq.Expressions;
using JobBoard.Data.Models;
using JobBoard.Data.ViewModels;
using JobBoard.Repository.Interfaces;
using JobBoard.Services.Interfaces;

namespace JobBoard.Services.Implementations;

public class JobServices : IJobServices
{

    private readonly IGenericRepository<Job> _jobGenericRepository;
    private readonly IGenericRepository<Users> _userGenericRepository;

    public JobServices(IGenericRepository<Job> jobGenericRepository, IGenericRepository<Users> userGenericRepository)
    {
        _jobGenericRepository = jobGenericRepository;
        _userGenericRepository = userGenericRepository;
    }

    public async Task<JobForm?> ReadAsync(int jobId)
    {
        try
        {
            Expression<Func<Job, bool>> searchExpression = c => c.Id == jobId;
            Job data = await _jobGenericRepository.Read(searchExpression, [c => c.Employer, c => c.Employer.Companies]);
            JobForm formData = new()
            {
                Id = jobId,
                CategoryId = data.CategoryId,
                CountryId = data.CountryId,
                CityId = data.CityId,
                StateId = data.StateId,
                Title = data.Title,
                Description = data.Description,
                Deadline = data.Deadline,
                Salary = data.Salary,
                MinExperience = data.MinExperience,
                CompanyName = data.Employer.Companies.First().Name,
                CompanyLogo = data.Employer.Companies.First().Logo,
            };
            return formData;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<JobForm?> ReadEmployerAsync(int employerId)
    {
        Expression<Func<Users, bool>> searchExpression = c => c.Id == employerId;
        // Job data = await _jobGenericRepository.Read(searchExpression, [c => c.Employer, c => c.Employer.Companies]);
        Users employer = await _userGenericRepository.Read(filter: searchExpression, includes: [c => c.Companies]);
        if (employer == null)
        {
            return null;
        }
        else
        {
            JobForm formData = new()
            {
                CompanyName = employer.Companies.First().Name,
                CompanyLogo = employer.Companies.First().Logo,
                CityId = employer.CityId,
                CountryId = employer.CountryId,
                StateId = employer.StateId,
            };
            return formData;
        }
    }

    public async Task<string?> AddJobAsync(JobForm jobForm)
    {
        Job data = new()
        {
            CityId = jobForm.CityId,
            StateId = jobForm.StateId,
            CountryId = jobForm.CountryId,
            CategoryId = jobForm.CategoryId,
            Title = jobForm.Title,
            Description = jobForm.Description,
            Salary = jobForm.Salary,
            MinExperience = jobForm.MinExperience,
            Deadline = DateTime.SpecifyKind(jobForm.Deadline.AddHours(23).AddMinutes(59), DateTimeKind.Utc),
            EmployerId = jobForm.EmployerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            CreatedBy = jobForm.EmployerId,
            UpdatedBy = jobForm.EmployerId,
        };

        return await _jobGenericRepository.Add(data);
    }

    public async Task<string?> UpdateJobAsync(JobForm jobForm)
    {
        Expression<Func<Job, bool>> expression = c => c.Id == jobForm.Id;
        Job currentdata = await _jobGenericRepository.Read(expression);
        currentdata.CityId = jobForm.CityId;
        currentdata.StateId = jobForm.StateId;
        currentdata.CountryId = jobForm.CountryId;
        currentdata.Description = jobForm.Description;
        currentdata.Salary = jobForm.Salary;
        currentdata.MinExperience = jobForm.MinExperience;
        currentdata.Deadline = DateTime.SpecifyKind(jobForm.Deadline.AddHours(23).AddMinutes(59), DateTimeKind.Utc);
        currentdata.EmployerId = jobForm.EmployerId;
        currentdata.UpdatedAt = DateTime.UtcNow;
        currentdata.UpdatedBy = jobForm.EmployerId;

        return _jobGenericRepository.Update(currentdata).Result;
    }

    public async Task<string?> DeleteJob(int jobId)
    {
        try
        {
            Job data = await _jobGenericRepository.Read(filter: c => c.Id == jobId);
            if (data == null)
            {
                return "not job with specific id found";
            }
            else
            {
                data.IsDeleted = true;
                string message = await _jobGenericRepository.Update(data);
                if (string.IsNullOrEmpty(message))
                {
                    return "Action perfromed successfually";
                }
                else
                {
                    return message;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return "Excepetion occured";
        }
    }

    public async Task<List<AdminJobView>> GetUnapprovedJobs()
    {
        List<Job> jobs = await _jobGenericRepository.ReadAllWithFilter(filter: c => !c.IsDeleted,
                                                    includes: [c => c.Employer, c => c.Employer.Companies, c => c.Category]);
        if (jobs.Count > 0)
        {
            List<AdminJobView> data = jobs.Select(c => new AdminJobView()
            {
                Id = c.Id,
                Title = c.Title,
                Category = c.Category.Name,
                EmployerName = (string)c.Employer.FName.Concat(c.Employer.LName),
                CompanyName = c.Employer.Companies.First().Name,
                CompanyLogo = c.Employer.Companies.First().Logo
            }).ToList();

            return data;
        }
        else
        {
            return [];
        }

    }

    public async Task<List<EmployerJobList>> JobListEmployer(int employerId)
    {
        try
        {
            List<Job> jobs = await _jobGenericRepository.ReadAllWithFilter(
                filter: c => !c.IsDeleted && c.EmployerId == employerId, includes: [c => c.Category, c => c.Applicants]);
            List<EmployerJobList> data = jobs.Select(c => new EmployerJobList()
            {
                Id = c.Id,
                Title = c.Title,
                Category = c.Category.Name,
                Deadline = c.Deadline,
                Status = c.Status,
                Applicants = c.Applicants.Count
            }).ToList();

            return data;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<Paginate<LandingJobView>> GetPaginatedJobs(PaginationInputs inputs)
    {   
        // dealine left for landing page and user side
        Paginate<LandingJobView> data = new();
        List<Job> jobs = await _jobGenericRepository.ReadAllWithFilter(
                filter: c => !c.IsDeleted && c.Status, includes: [c => c.Category, c => c.Country, c => c.State, c => c.City, c => c.Employer.Companies]);
        data.Items = jobs.Select(c => new LandingJobView()
        {
            Id = c.Id,
            Title = c.Title,
            Description = c.Description,
            CompanyLogo = c.Employer.Companies.First().Logo,
            CompanyName = c.Employer.Companies.First().Name,
            Country = c.Country.Name,
            State = c.State.Name,
            City = c.City.Name,
            PostedAt = c.CreatedAt,
            Category = c.Category.Name,
        }).Skip((inputs.Page - 1) * inputs.PageSize).Take(inputs.PageSize).ToList();

        data.PageIndex = inputs.Page;
        data.PageSize = inputs.PageSize;
        data.Search = inputs.Search;
        data.TotalItems = jobs.Count;
        data.TotalPages = (int)Math.Ceiling(data.TotalItems / (double)inputs.PageSize);
        return data;
    }

    public async Task<Paginate<EmployerJobList>> GetJobs(PaginationInputs inputs)
    {   
        // dealine left for landing page and user side
        Paginate<EmployerJobList> data = new();
        List<Job> jobs = await _jobGenericRepository.ReadAllWithFilter(
                filter: c => !c.IsDeleted && c.Status, includes: [c => c.Category, c => c.Applicants]);
        data.Items = jobs.Select(c => new EmployerJobList()
        {
            Id = c.Id,
            Title = c.Title,
            Deadline = c.Deadline,
            Category = c.Category.Name,
            Status = c.Status,
            Applicants = c.Applicants.Count
        }).Skip((inputs.Page - 1) * inputs.PageSize).Take(inputs.PageSize).ToList();

        data.PageIndex = inputs.Page;
        data.PageSize = inputs.PageSize;
        data.Search = inputs.Search;
        data.TotalItems = jobs.Count;
        data.TotalPages = (int)Math.Ceiling(data.TotalItems / (double)inputs.PageSize);
        return data;
    }


}
