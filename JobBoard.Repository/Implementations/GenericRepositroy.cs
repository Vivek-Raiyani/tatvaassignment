using System.Linq.Expressions;
using JobBoard.Data.Context;
using JobBoard.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Repository.Implementations;

public class GenericRepositroy<T> : IGenericRepository<T> where T : class
{
    private readonly JobBoardContext _context;
    public GenericRepositroy(JobBoardContext context)
    {
        _context = context;
    }


    public async Task<string> Add(T obj)
    {
        try
        {
            _context.Add(obj);
            await _context.SaveChangesAsync();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return string.Empty;
    }

    public async Task<string> Update(T obj)
    {
        try
        {
            _context.Update(obj);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return string.Empty;
    }

    public async Task<List<T>> ReadAll(params Expression<Func<T, object>>[] includes)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return [];
    }

    public async Task<List<T>> ReadAllWithFilter(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.Where(filter).ToListAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return [];
    }

    public async Task<T?> Read(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
    {
        try
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null && includes.Length != 0)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return await query.SingleOrDefaultAsync(filter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        return null;
    }

}
