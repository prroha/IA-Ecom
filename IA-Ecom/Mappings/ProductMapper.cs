using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Mappers;

public class ProductMapper
{
    public static ProductViewModel MapToViewModel(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        return new ProductViewModel
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock,
            Category = product.Category,
            Color = product.Color,
            ThumbnailImageUrl = product.ThumbnailImageUrl,
            EntryDate = product.EntryDate,
            ImageUrls = product.ProductImages != null
                ? product.ProductImages.Select(pi => pi.ImageUrl).ToList()
                : new List<string>()
        };
    }

    public static Product MapToModel(ProductViewModel viewModel)
    {
        return new Product
        {
            ProductId = viewModel.ProductId,
            Name = viewModel.Name,
            Description = viewModel.Description,
            Category = viewModel.Category,
            Price = viewModel.Price,
            Stock = viewModel.Stock,
            Color = viewModel.Color,
            ThumbnailImageUrl = viewModel.ThumbnailImageUrl,
            OrderItems = new List<OrderItem>() // Initialize empty list, if needed
        };
    }
}