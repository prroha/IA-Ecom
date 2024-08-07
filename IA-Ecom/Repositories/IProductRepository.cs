using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
// Product Image methods
        Task AddProductImagesAsync(IEnumerable<ProductImage> images);
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);    }
}