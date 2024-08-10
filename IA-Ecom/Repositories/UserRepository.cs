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

        public async Task<IList<User>> GetAllAsync()
        {
            return await context.Users.Where(u => u.DeletedDate != null).ToListAsync();
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(User user)
        {
                // var entityType = user.GetType();
                // var properties = entityType.GetProperties();
                // foreach (var property in properties)
                // {
                //     context.Entry(existingEntity).Property(property.Name).IsModified = true;
                // }

                context.Update(user);
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