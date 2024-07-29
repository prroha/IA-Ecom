using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Mappers;

public class ProductMapper
{
    public static ProductViewModel MapToViewModel(Product product)
    {
        return new ProductViewModel
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            Stock = product.Stock
        };
    }

    public static Product MapToModel(ProductViewModel viewModel)
    {
        return new Product
        {
            ProductId = viewModel.ProductId,
            Name = viewModel.Name,
            Description = viewModel.Description,
            Price = viewModel.Price,
            Stock = viewModel.Stock,
            OrderItems = new List<OrderItem>() // Initialize empty list, if needed
        };
    }
}