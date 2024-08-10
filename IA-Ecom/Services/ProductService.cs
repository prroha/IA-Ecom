using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;

namespace IA_Ecom.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IWebHostEnvironment _environment;

        public ProductService(IProductRepository productRepository, IWebHostEnvironment environment)
        {
            _productRepository = productRepository;
            _environment = environment;
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
            Product product = await _productRepository.GetByIdAsync(id);
            product.ProductImages = (await _productRepository.GetProductImagesAsync(product.ProductId))?.ToList();
            return product;
        }

        public async Task AddProductAsync(Product product, List<IFormFile> images)
        {
            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            // Save images
            if (images != null && images.Count > 0)
            {
                await SaveImageAsync(images, product.ProductId, new List<ProductImage>());
            await _productRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateProductAsync(Product product, List<IFormFile> images)
        {
            // _productRepository.Update(product);
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
                await SaveImageAsync(images, product.ProductId, existingImages.ToList());
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

        private async Task SaveImageAsync(List<IFormFile> images, int productId, List<ProductImage> existingImages )
        {
            
            if (images != null && images.Count > 0)
            {
                var productImages = new List<ProductImage>();

                foreach (var image in images)
                {
                    var filePath = await StoreImageAsync(image);

                    if (existingImages.Any(img => img.ImageUrl == filePath))
                    {
                        // Skip adding existing images again
                        continue;
                    }

                    productImages.Add(new ProductImage
                    {
                        ImageName = image.FileName,
                        ImageUrl = filePath,
                        ProductId = productId
                    });
                }

                if (productImages.Any())
                {
                    // product.ThumbnailImageUrl = productImages[0]?.ImageUrl;
                    await _productRepository.AddProductImagesAsync(productImages);
                }
            }
        }
        private async Task<string> StoreImageAsync(IFormFile image)
        {
            var uploadDir = Path.Combine(_environment.ContentRootPath, "App_Data","Objects");
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

            return $"/Objects/{fileName}";
        }

        private async Task RemoveImagesAsync(List<ProductImage> images)
        {
            if (images.Any())
            {
                foreach (var image in images)
                {
                    // Construct the full file path for each image to remove
                    var filePath = Path.Combine(_environment.ContentRootPath, image.ImageUrl.TrimStart('/'));

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