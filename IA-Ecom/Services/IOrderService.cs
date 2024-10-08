using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.RequestModels;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Services
{
    public interface IOrderService
    {
        Task<int> CountAllAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<OrderItem> GetOrderItemByIdAsync(int id);
        Task<Order> GetOrderByIdAsync(int id);
        Task<Order> GetCartDetailsAsync(string customerId);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        Task DeleteOrderItemAsync(int id);
        Task<bool> ProcessOrderAsync(int orderId, string customerId);
        Task AddToCartAsync(OrderItem orderItem, User user);
        void AddCartItemToOrder(Order order, CartItem cartItem);
        void PlaceOrder(Order order);
        void ClearCart(string customerId);
        Task<bool> CheckoutAsync(string customerId, PaymentMethod paymentMethod);
    }
}