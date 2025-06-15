using System.Linq.Expressions;
using JobBoard.Data.Models;
using JobBoard.Data.ViewModels;
using JobBoard.Repository.Interfaces;
using JobBoard.Services.Interfaces;
using JobBoard.Services.Utils;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace JobBoard.Services.Implementations;

public class UserServices : IUserServices
{
    private readonly IGenericRepository<Users> _genericRepository;
    public UserServices(IGenericRepository<Users> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public string RegisterEmployer(RegisterEmployer modal)
    {
        try
        {
            Users user = new()
            {
                CityId = modal.CityId,
                StateId = modal.StateId,
                CountryId = modal.CountryId,
                RoleId = 2,
                Email = modal.Email,
                FName = modal.FName,
                LName = modal.LName,
                Profile = modal.ProfileImg,
                ZipCode = modal.ZipCode,
                Address = modal.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
            };

            string passwordHash = PasswordHash.PassHash(modal.Password, user);
            user.Password = passwordHash;

            Company company = new()
            {
                Name = modal.CompanyName,
                Logo = modal.LogoPath,
                Details = modal.CompanyDetails
            };
            user.Companies.Add(company);

            return _genericRepository.Add(user).Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
    }

    public string RegisterJobSeeker(RegisterJobSeaker modal)
    {
        try
        {

            Users user = new()
            {
                CityId = modal.CityId,
                StateId = modal.StateId,
                CountryId = modal.CountryId,
                RoleId = 3,
                Email = modal.Email,
                FName = modal.FName,
                LName = modal.LName,
                ZipCode = modal.ZipCode,
                Address = modal.Address,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                IsActive = true,
            };

            string passwordHash = PasswordHash.PassHash(modal.Password, user);
            user.Password = passwordHash;

            JobSeeker jobSeeker = new()
            {
                DOB = modal.DOB,
                Phone = modal.Phone,
            };

            user.JobSeakers.Add(jobSeeker);

            return _genericRepository.Add(user).Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return string.Empty;
        }
    }

    public Users? GetUser(string email)
    {
        try
        {
            Expression<Func<Users, bool>> filter = u => u.Email == email ;
            Expression<Func<Users, object>> include = u => u.Role;
            return _genericRepository.Read(filter: filter, includes: include).Result;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return null;
        }
    }

    public async Task<List<AdminEmployerView>> GetUnapprovedEmployer()
    {
        try
        {
            List<Users> users = await _genericRepository.ReadAllWithFilter(filter: c => !c.IsDeleted && !c.IsActive && c.RoleId == 2, includes: c => c.Companies);
            if (users.Count > 0)
            {
                List<AdminEmployerView> data = users.Select(c => new AdminEmployerView()
                {
                    Id = c.Id,
                    Name = string.Concat(c.FName, "", c.LName),
                    Profile = c.Profile,
                    Email = c.Email,
                    CompanyName = c.Companies.First().Name,
                    CompanyLogo = c.Companies.First().Logo,
                }).ToList();

                return data;
            }

            return [];
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<string> ApproveEmployer(int employerId)
    {
        try
        {
            Users employer = await _genericRepository.Read(filter: c => c.Id == employerId);
            if (employer == null)
            {
                return "user not found";
            }
            else
            {
                employer.IsActive = true;
                return await _genericRepository.Update(employer);
            }
        }
        catch (Exception ex)
        {
            return "error occured";
        }
    }
}
