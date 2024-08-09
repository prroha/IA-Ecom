using System.Linq.Expressions;
using IA_Ecom.Data;
using IA_Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class UserRepository(ApplicationDbContext context) :  IUserRepository
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

        public Task<IList<User>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> FindAsync(Expression<Func<User, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(User entity)
        {
            throw new NotImplementedException();
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(User entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
}