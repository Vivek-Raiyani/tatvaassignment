using JobBoard.Data.Models;
using JobBoard.Data.ViewModels;

namespace JobBoard.Services.Interfaces;

public interface IUserServices
{
     public string RegisterEmployer(RegisterEmployer modal);

     public string RegisterJobSeeker(RegisterJobSeaker modal);

     public Users? GetUser(string email);

     public Task<List<AdminEmployerView>> GetUnapprovedEmployer();

     public Task<string> ApproveEmployer(int employerId);
}
