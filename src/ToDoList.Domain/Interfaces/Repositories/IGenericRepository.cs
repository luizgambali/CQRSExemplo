using System.Linq.Expressions;

namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIDAsync(Guid id);    
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetByCriteria(Expression<Func<T, bool>> criteria);
        void Add(T item);
        void Update(T item);    
        Task CommitAsync();
    }
}