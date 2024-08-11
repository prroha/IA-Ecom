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
            CustomerName = order.Customer?.FullName, 
            OrderDate = order.OrderDate,
            Status = order.Status,
            TotalAmount = order.TotalAmount,
            OrderItems = order.OrderItems?.Select(MapToViewModel)?.ToList() ?? new List<OrderItemViewModel>()
        };
    }

    public static Order MapToModel(OrderViewModel viewModel, User customer)
    {
        return new Order
        {
            Id = viewModel.OrderId,
            Customer = customer,
            OrderDate = viewModel.OrderDate,
            Status = viewModel.Status,
            TotalAmount = viewModel.TotalAmount,
            OrderItems = viewModel.OrderItems?.Select(MapToModel)?.ToList()
        };
    }

    public static OrderItemViewModel MapToViewModel(OrderItem orderItem)
    {
        return new OrderItemViewModel
        {
            OrderItemId = orderItem.OrderItemId,
            ProductId = orderItem.ProductId,
            ProductName = orderItem.Product.Name,
            ProductSize = orderItem.ProductSize,
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice
        };
    }

    public static OrderItem MapToModel(OrderItemViewModel viewModel)
    {
        return new OrderItem
        {
            Id = viewModel.OrderItemId,
            ProductId = viewModel.ProductId,
            ProductSize = viewModel.ProductSize,
            Quantity = viewModel.Quantity,
            UnitPrice = viewModel.UnitPrice
        };
    }
    
    public static OrderConfirmationViewModel MapToViewModel(Payment payment, Order order)
    {
        return new OrderConfirmationViewModel
        {
            OrderId = payment.OrderId,
            OrderDate = order.OrderDate,
            TotalAmount = order.TotalAmount,
            PaymentMethod = payment.PaymentMethod,
            OrderItems = order.OrderItems?.Select(MapToViewModel)?.ToList()
        };
    }
}