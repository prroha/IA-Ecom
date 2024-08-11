using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Repositories;
using IA_Ecom.RequestModels;

namespace IA_Ecom.Services
{
    public class OrderService(IOrderRepository orderRepository, IPaymentService paymentService) : IOrderService
    {
        public async Task<int> CountAllAsync()
        {
            return await orderRepository.CountAllAsync();
        }
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await orderRepository.GetAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await orderRepository.GetByIdAsync(id);
        }
        public async Task<Order> GetCartDetailsAsync(string customerId)
        {
            return await orderRepository.GetCartDetailsAsync(customerId);
        }

        public async Task AddOrderAsync(Order order)
        {
            await orderRepository.AddAsync(order);
            await orderRepository.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            orderRepository.Update(order);
            await orderRepository.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await orderRepository.GetByIdAsync(id);
            if (order != null)
            {
                orderRepository.Remove(order);
                await orderRepository.SaveChangesAsync();
            }
        }
        public async Task<bool> CheckoutAsync(string customerId, PaymentMethod paymentMethod)
        {
            Order order = await orderRepository.GetOrderByCustomerIdAsync(customerId);
            if (order == null)
            {
                return false; // Empty Cart
            }

            bool paymentSuccessful = await paymentService.ProcessPayment(order, paymentMethod);

            if (!paymentSuccessful)
            {
                return false;
            }

            // Finalize the order
            order.Status = "Finalized";
            order.OrderDate = DateTime.UtcNow;
            await orderRepository.SaveChangesAsync();

            return true;
        }

        public Order GetOrderById(int id)
        {
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

            orderRepository.AddAsync(order);
        }
            
        public async Task<bool> ProcessOrderAsync(int orderId, string customerId)
        {
            var order = await GetOrderByIdAsync(orderId);
            if (order == null)
            {
                return false;
            }

            // var cartItems = GetCartItems(customerId);
            // foreach (var cartItem in cartItems)
            // {
            // AddCartItemToOrder(order, cartItem);
            // }

            PlaceOrder(order);
            ClearCart(customerId);

            return true;
        }

        public Task AddToCartAsync(OrderItem orderItem, User user)
        {
            return orderRepository.AddToCartAsync(orderItem, user);
        }

        public void AddCartItemToOrder(Order order, CartItem cartItem)
        {
            orderRepository.AddCartItemToOrder(order, cartItem);
        }

        public void ClearCart(string customerId)
        {
            orderRepository.ClearCart(customerId);
        } 
    }

}