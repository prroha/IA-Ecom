using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;

namespace IA_Ecom.Services
{
    public interface IProductService
    {
        Task<int> CountAllAsync();
        Task<IEnumerable<Product>> GetAllProductsAsync();
        Task<Product> GetProductByIdAsync(int id);
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(int id);
    }
}