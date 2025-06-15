using System.Linq.Expressions;

namespace JobBoard.Services.Interfaces;

public interface IGenericServices<T> where T : class
{
    public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);

    public Task<T?> GetAsync(Expression<Func<T, bool>> filter);

    public Task<string> AddAsync(T obj);

    public Task<string> UpdateAsync(T obj);

    public Task<List<T>> GetAllWithFilterAsync(Expression<Func<T, bool>> filter,params Expression<Func<T, object>>[] includes);

}
