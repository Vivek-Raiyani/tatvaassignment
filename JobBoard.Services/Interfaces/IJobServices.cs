using JobBoard.Data.ViewModels;

namespace JobBoard.Services.Interfaces;

public interface IJobServices
{
    public Task<JobForm?> ReadAsync(int jobId);

    public Task<JobForm?> ReadEmployerAsync(int employerId);

    public Task<string?> AddJobAsync(JobForm jobForm);

    public Task<string?> UpdateJobAsync(JobForm jobForm);

    public Task<string?> DeleteJob(int jobId);

    public Task<List<AdminJobView>> GetUnapprovedJobs();

    public Task<List<EmployerJobList>> JobListEmployer(int employerId);

    public Task<Paginate<LandingJobView>> GetPaginatedJobs(PaginationInputs inputs);

    public Task<Paginate<EmployerJobList>> GetJobs(PaginationInputs inputs);

}
