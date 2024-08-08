using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IA_Ecom.Data;
using IA_Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly ApplicationDbContext _context;

        protected GenericRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> CountAllAsync()
        {
            return await _context.Set<T>().CountAsync();
        }
        public async Task<T> GetByIdAsync(int id)
        {
            // var entity = await _context.Set<T>().FindAsync(id);
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id && e.DeletedDate == null);
            if (entity == null)
            {
                throw new InvalidOperationException($"Entity of type {typeof(T)} with id {id} not found.");
            }
            return entity;
        }

        public async Task<IList<T>> GetAllAsync()
        {
            // return await _context.Set<T>().ToListAsync();
            return await _context.Set<T>()
                .Where(entity => entity.DeletedDate == null)
                .ToListAsync();
        }

        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().Where(predicate).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
            entity.DeletedDate = DateTime.UtcNow;
            await SaveChangesAsync();
            }

        }
        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
