using IA_Ecom.Data;
using IA_Ecom.Models;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = context;

// Add item to shopping cart (Order)
    public async Task AddCartItemToOrderAsync(int orderId, CartItem cartItem)
    {
        var order = await GetOrderByIdAsync(orderId);
        if (order != null)
        {
            order.OrderItems.Add(new OrderItem
            {
                ProductId = cartItem.ProductId,
                Quantity = cartItem.Quantity,
                UnitPrice = cartItem.UnitPrice
            });

            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
    public async Task<Order> CreateOrderFromCartAsync(string customerId)
    {
        var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
            .ThenInclude(ci => ci.Product)
            .FirstOrDefaultAsync(c => c.CustomerId == customerId);

        if (cart == null)
        {
            throw new InvalidOperationException("Cart not found.");
        }

        var order = new Order
        {
            CustomerId = cart.CustomerId,
            OrderDate = DateTime.UtcNow,
            Status = "Pending",
            ShippingAddress = "Sample Address", // This should be taken from user input
            OrderItems = cart.CartItems.Select(ci => new OrderItem
            {
                ProductId = ci.ProductId,
                Quantity = ci.Quantity,
                UnitPrice = ci.UnitPrice
            }).ToList(),
            TotalAmount = cart.CartItems.Sum(ci => ci.TotalPrice)
        };

        _dbContext.Orders.Add(order);

        // Clear the cart after order is created
        _dbContext.Carts.Remove(cart);

        await _dbContext.SaveChangesAsync();

        return order;
    }

    // Remove item from shopping cart (Order)
    public async Task RemoveCartItemFromOrderAsync(int orderId, int cartItemId)
    {
        var order = await GetOrderByIdAsync(orderId);
        if (order != null)
        {
            var orderDetail = order.OrderItems.FirstOrDefault(od => od.OrderItemId == cartItemId);
            if (orderDetail != null)
            {
                order.OrderItems.Remove(orderDetail);
                _dbContext.Orders.Update(order);
                await _dbContext.SaveChangesAsync();
            }
        }
    }

    // Clear shopping cart (Order)
    public async Task ClearCartAsync(int orderId)
    {
        var order = await GetOrderByIdAsync(orderId);
        if (order != null)
        {
            order.OrderItems.Clear();
            _dbContext.Orders.Update(order);
            await _dbContext.SaveChangesAsync();
        }
    }
    public IEnumerable<CartItem> GetCartItems(string customerId)
    {
        var cart = _dbContext.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.CustomerId == customerId);

        return cart?.CartItems ?? new List<CartItem>();
    }

    public void AddCartItemToOrder(Order order, CartItem cartItem)
    {
        order.OrderItems.Add(new OrderItem
        {
            ProductId = cartItem.ProductId,
            Quantity = cartItem.Quantity,
            UnitPrice = cartItem.UnitPrice,
            // Other order detail fields as needed
        });
    }

    public void PlaceOrder(Order order)
    {
        _dbContext.Orders.Add(order);
        _dbContext.SaveChanges();
    }

    public void ClearCart(string customerId)
    {
        var cart = _dbContext.Carts
            .Include(c => c.CartItems)
            .FirstOrDefault(c => c.CustomerId == customerId);

        if (cart != null)
        {
            _dbContext.CartItems.RemoveRange(cart.CartItems);
            _dbContext.Carts.Remove(cart);
            _dbContext.SaveChanges();
        }
    }
    // Process payment
    public async Task<bool> ProcessPaymentAsync(Payment payment)
    {
        // Implement payment processing logic here (e.g., using a payment gateway API)
        // For now, assume the payment is always successful
        await _dbContext.Payments.AddAsync(payment);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    // Get payment by ID
    public async Task<Payment> GetPaymentByIdAsync(int paymentId)
    {
        return await _dbContext.Payments.FirstOrDefaultAsync(p => p.PaymentId == paymentId);
    }
        private async Task<Order> GetOrderByIdAsync(int orderId)
        {
            return await GetByIdAsync(orderId);
        }
        // Implement additional methods specific to Order if any
    }
}