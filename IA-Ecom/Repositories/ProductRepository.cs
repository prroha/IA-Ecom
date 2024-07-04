using IA_Ecom.Data;
using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }

        // Implement additional methods specific to Product if any
    }
}