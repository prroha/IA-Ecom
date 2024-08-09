using System.Linq.Expressions;
using IA_Ecom.Data;
using IA_Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public async Task<User> GetUserByUserIdAsync(string userId)
        {
            return await context.Users.FindAsync(userId);
        }

        public async Task<int> CountAllAsync()
        {
            return await context.Users.CountAsync();
        }

        public Task<User> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<User>> GetAllAsync()
        {
            return await context.Users.Where(u => u.DeletedDate != null).ToListAsync();
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User user)
        {
            var existingEntity = await context.Users.FindAsync(user.Id);
            if (existingEntity != null)
            {
                var entityType = user.GetType();
                var properties = entityType.GetProperties();
                foreach (var property in properties)
                {
                    context.Entry(existingEntity).Property(property.Name).IsModified = true;
                }

                // context.Update(user);
                await context.SaveChangesAsync();
                
            }
            
                }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }


        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task DeleteAsync(string id)
        {
            User user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user != null)
            {
                user.DeletedDate = DateTime.UtcNow;
            }
        }

        public async Task SaveChangesAsync()
        {
            await context.SaveChangesAsync();
        }
    }
}