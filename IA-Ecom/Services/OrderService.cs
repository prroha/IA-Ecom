using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;

namespace IA_Ecom.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<int> CountAllAsync()
        {
            return await _orderRepository.CountAllAsync();
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _orderRepository.GetByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _orderRepository.AddAsync(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _orderRepository.Update(order);
            await _orderRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                _orderRepository.Remove(order);
                await _orderRepository.SaveChangesAsync();
            }
        }

            public IEnumerable<Order> GetAllOrders()
            {
                // Implementation to fetch all orders from database
                throw new NotImplementedException();
            }

            public Order GetOrderById(int id)
            {
                // Implementation to fetch order by id from database
                throw new NotImplementedException();
            }

            public void PlaceOrder(Order order)
            {
                if (order == null)
                {
                    throw new ArgumentNullException(nameof(order));
                }

                // Example business logic before saving order
                // e.g., Calculate total, validate order, etc.

                _orderRepository.AddAsync(order);
            }
            
            public async Task<bool> ProcessOrderAsync(int orderId, string customerId)
            {
                var order = await GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return false;
                }

                var cartItems = GetCartItems(customerId);
                foreach (var cartItem in cartItems)
                {
                    AddCartItemToOrder(order, cartItem);
                }

                PlaceOrder(order);
                ClearCart(customerId);

                return true;
            }
            public void AddCartItemToOrder(Order order, CartItem cartItem)
            {
                _orderRepository.AddCartItemToOrder(order, cartItem);
            }
            public IEnumerable<CartItem> GetCartItems(string customerId)
            {
                return _orderRepository.GetCartItems(customerId);
            }

            public void ClearCart(string customerId)
            {
                _orderRepository.ClearCart(customerId);
            } 
    }

}