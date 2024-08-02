using IA_Ecom.Models;

namespace IA_Ecom.Repositories
{
    public interface IOrderRepository : IGenericRepository<Order>
    {
        // Define additional methods specific to Order if any
        // IEnumerable<CartItem> GetCartItems(string customerId);
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task AddToCartAsync(OrderItem orderItem);
        Task<Order> GetOrderByCustomerIdAsync(string customerId);
        Task<Order> GetCartDetailsAsync(string customerId);
        void AddCartItemToOrder(Order order, CartItem cartItem);
        void PlaceOrder(Order order);
        void ClearCart(string customerId);
    }
}