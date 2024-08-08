using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;

namespace IA_Ecom.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductService(IProductRepository productRepository,IWebHostEnvironment webHostEnvironment)
        {
            _productRepository = productRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<int> CountAllAsync()
        {
            return await _productRepository.CountAllAsync();
        }
        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _productRepository.GetAllAsync();
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _productRepository.GetByIdAsync(id);
        }

        public async Task AddProductAsync(Product product, List<IFormFile> images)
        {
            await _productRepository.AddAsync(product);
            // Save images
            if (images != null && images.Count > 0)
            {
                var productImages = new List<ProductImage>();
                foreach (var image in images)
                {
                    var filePath = await SaveImageAsync(image);
                    productImages.Add(new ProductImage
                    {
                        ImageUrl = filePath,
                        ProductId = product.ProductId
                    });
                }
                await _productRepository.AddProductImagesAsync(productImages);
            }
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _productRepository.Update(product);
            await _productRepository.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product != null)
            {
                await _productRepository.DeleteAsync(product.ProductId);
                await _productRepository.SaveChangesAsync();
            }
        }
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            var uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "App_Data/Objects");
            if (!Directory.Exists(uploadDir))
            {
                Directory.CreateDirectory(uploadDir);
            }

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            var filePath = Path.Combine(uploadDir, fileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }

            return $"/App_Data/Objects/{fileName}";
        }
    }
}