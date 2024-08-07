using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IA_Ecom.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<int> CountAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<IList<T>> GetAllAsync();
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task SaveChangesAsync();
    }
}
