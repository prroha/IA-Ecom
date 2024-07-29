using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Mappers;

public class OrderMapper
{
    public static OrderViewModel MapToViewModel(Order order)
    {
        return new OrderViewModel
        {
            OrderId = order.OrderId,
            CustomerName = order.Customer.FullName, // Assuming Customer has a CustomerName property
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            // ShippingAddress = order.ShippingAddress,
            // City = order.City,
            // State = order.State,
            // PostalCode = order.PostalCode,
            // Country = order.Country,
            OrderItems = order.OrderItems.Select(MapToViewModel).ToList()
        };
    }

    public static Order MapToModel(OrderViewModel viewModel, User customer)
    {
        return new Order
        {
            OrderId = viewModel.OrderId,
            Customer = customer,
            OrderDate = viewModel.OrderDate,
            Status = viewModel.Status,
            TotalAmount = viewModel.TotalAmount,
            // ShippingAddress = viewModel.ShippingAddress,
            // City = viewModel.City,
            // State = viewModel.State,
            // PostalCode = viewModel.PostalCode,
            // Country = viewModel.Country,
            OrderItems = viewModel.OrderItems.Select(MapToModel).ToList()
        };
    }

    private static OrderItemViewModel MapToViewModel(OrderItem orderItem)
    {
        return new OrderItemViewModel
        {
            OrderItemId = orderItem.OrderItemId,
            ProductName = orderItem.Product.Name, // Assuming OrderItem has a ProductName property
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice
        };
    }

    private static OrderItem MapToModel(OrderItemViewModel viewModel)
    {
        return new OrderItem
        {
            OrderItemId = viewModel.OrderItemId,
            Quantity = viewModel.Quantity,
            UnitPrice = viewModel.UnitPrice
        };
    }
}