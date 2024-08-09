using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;

namespace IA_Ecom.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
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
                        ImageName = image.FileName,
                        ImageUrl = filePath,
                        ProductId = product.ProductId
                    });
                }
                await _productRepository.AddProductImagesAsync(productImages);
            }
            await _productRepository.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product, List<IFormFile> images)
        {
            _productRepository.Update(product);
            // Load existing images from the database
            var existingImages = await _productRepository.GetProductImagesAsync(product.ProductId);
        
            // Remove images that are no longer present
            var imagesToRemove = existingImages
                .Where(img => !images.Any(newImg => newImg.FileName == img.ImageName))
                .ToList();

            if (imagesToRemove.Any())
            {
                await RemoveImagesAsync(imagesToRemove);
                await _productRepository.RemoveProductImagesAsync(imagesToRemove);
            }
            // Handle new images
            if (images != null && images.Count > 0)
            {
                var productImages = new List<ProductImage>();

                foreach (var image in images)
                {
                    var filePath = await SaveImageAsync(image);

                    if (existingImages.Any(img => img.ImageUrl == filePath))
                    {
                        // Skip adding existing images again
                        continue;
                    }

                    productImages.Add(new ProductImage
                    {
                        ImageName = image.FileName,
                        ImageUrl = filePath,
                        ProductId = product.ProductId
                    });
                }

                if (productImages.Any())
                {
                    await _productRepository.AddProductImagesAsync(productImages);
                }
            }
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
            var uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "App_Data/Objects");
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

        private async Task RemoveImagesAsync(List<ProductImage> images)
        {
            if (images.Any())
            {
                foreach (var image in images)
                {
                    // Construct the full file path for each image to remove
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), image.ImageUrl.TrimStart('/'));

                    // Check if the file exists and delete it
                    if (File.Exists(filePath))
                    {
                        File.Delete(filePath);
                    }
                }
            }
            }
    }
}