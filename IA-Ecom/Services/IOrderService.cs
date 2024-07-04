using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;

namespace IA_Ecom.Services
{
    public interface IOrderService
    {
        Task<int> CountAllAsync();
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
        IEnumerable<CartItem> GetCartItems(string customerId);
        Task<bool> ProcessOrderAsync(int orderId, string customerId);
        void AddCartItemToOrder(Order order, CartItem cartItem);
        void PlaceOrder(Order order);
        void ClearCart(string customerId);
    }
}