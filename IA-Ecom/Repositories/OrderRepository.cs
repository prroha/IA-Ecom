using IA_Ecom.Data;
using IA_Ecom.Models;
using IA_Ecom.RequestModels;
using Microsoft.EntityFrameworkCore;

namespace IA_Ecom.Repositories
{
    public class OrderRepository(ApplicationDbContext context) : GenericRepository<Order>(context), IOrderRepository
    {
        private readonly ApplicationDbContext _dbContext = context;

        public async Task AddToCartAsync(OrderItem orderItem, User user)
        {
            // Retrieve the current cart (order) for the user or create a new one
            var order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.CustomerId == orderItem.CustomerId && o.Status == "Cart");

            if (order == null)
            {
                order = new Order
                {
                    CustomerId = user.Id,
                    OrderDate = DateTime.UtcNow,
                    Status = "Cart",
                    PaymentStatus = PaymentStatus.Pending,
                    TotalAmount = 0,
                    ShippingAddress = user.Address,
                    OrderItems = new List<OrderItem>()
                };
                _dbContext.Orders.Add(order);
            }

            // Check if the product is already in the cart
            var orderItemDb = order.OrderItems.FirstOrDefault(oi => oi.ProductId == orderItem.ProductId);
            if (orderItemDb != null)
            {
                // Update quantity and price if product exists in cart
                orderItemDb.Quantity += orderItem.Quantity;
                orderItemDb.UnitPrice = orderItem.UnitPrice;
            }
            else
            {
                // Add new order item
                orderItemDb = new OrderItem
                {
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    UnitPrice = orderItem.UnitPrice,
                    CustomerId = user.Id,
                    ProductSize = orderItem.ProductSize
                };
                order.OrderItems.Add(orderItemDb);
            }

            // Update the total amount
            order.TotalAmount = order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            
        return _dbContext.Orders
            .Include(o => o.OrderItems);
        }
    public async Task<Order> CreateOrderFromCartAsync(string customerId)
    {
        var cart = await _dbContext.Carts
            .Include(c => c.CartItems)
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
    public async Task<Order> GetCartDetailsAsync(string customerId)
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.CustomerId == customerId && o.Status == "Cart");
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
        public async Task<Order> GetOrderByCustomerIdAsync(string customerId)
        {
            return  await _dbContext.Orders
                                .Include(o => o.OrderItems)
                                .FirstOrDefaultAsync(o => o.CustomerId ==customerId && o.Status == "Cart");
        }
    }
}