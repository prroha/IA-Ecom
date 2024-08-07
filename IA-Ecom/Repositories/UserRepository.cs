using IA_Ecom.Data;
using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public class UserRepository(ApplicationDbContext context) : GenericRepository<User>(context), IUserRepository
    {
        public async Task<User> GetUserByUserIdAsync(string userId)
        {
            return await context.Users.FindAsync(userId);
        }    }
}