using System.Linq.Expressions;

namespace JobBoard.Repository.Interfaces;

public interface IGenericRepository<T> where T : class
{
  public Task<string> Add(T obj);

  public Task<string> Update(T obj);

  public Task<List<T>> ReadAll(params Expression<Func<T, object>>[] includes);

  public Task<List<T>> ReadAllWithFilter(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

  public Task<T?> Read(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

}
