namespace Logiware.Domain.Contracts;

public interface IRepository<T> where T : class
{
    Task<List<T>> GetAll();
    Task<T?> GetById(int id);
    Task<T> Insert(T entity);
    Task Update(T entity);
    Task Delete(T entity);
}