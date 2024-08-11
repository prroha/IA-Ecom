using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task AddProductImagesAsync(IEnumerable<ProductImage> images);
        Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId);    
        Task RemoveProductImagesAsync(IEnumerable<ProductImage> images);
    }
}