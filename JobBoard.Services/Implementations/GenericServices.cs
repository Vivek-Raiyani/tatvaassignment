
using System.Linq.Expressions;
using JobBoard.Repository.Interfaces;
using JobBoard.Services.Interfaces;

namespace JobBoard.Services.Implementations;

public class GenericServices<T> : IGenericServices<T> where T : class
{
    private readonly IGenericRepository<T> _genericRepository;

    public GenericServices(IGenericRepository<T> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
    {
        try
        {
            return await _genericRepository.ReadAll(includes);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return [];
        }
    }

    public async Task<List<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter,params Expression<Func<T, object>>[] includes)
    {
        try
        {
            return await _genericRepository.ReadAllWithFilter(filter: filter, includes: includes);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return [];
        }
    }

    public async Task<T?> GetAsync(Expression<Func<T, bool>> filter)
    {
        try
        {
            return await _genericRepository.Read(filter: filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return null;
        }
    }

    public async Task<string> AddAsync(T obj)
    {
        try
        {
            return await _genericRepository.Add(obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return string.Empty;
        }
    }

    public async Task<string> UpdateAsync(T obj)
    {
        try
        {
            return await _genericRepository.Update(obj);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"{ex.Message}");
            return string.Empty;
        }
    }

}
