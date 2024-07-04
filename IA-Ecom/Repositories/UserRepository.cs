using IA_Ecom.Data;
using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implement additional methods specific to Customer if any
    }
}