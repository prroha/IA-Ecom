using IA_Ecom.Data;
using IA_Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class ProductRepository(ApplicationDbContext context)
        : GenericRepository<Product>(context), IProductRepository
    {
        public async Task AddProductImagesAsync(IEnumerable<ProductImage> images)
        {
            
            context.ProductImages.AddRange(images);
            await context.SaveChangesAsync();
        }

        public async Task RemoveProductImagesAsync(IEnumerable<ProductImage> images)
        {
            context.ProductImages.RemoveRange(images);
            await context.SaveChangesAsync();
        }
        public async Task<IEnumerable<ProductImage>> GetProductImagesAsync(int productId)
        {
            return await context.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();
        }
    }
}