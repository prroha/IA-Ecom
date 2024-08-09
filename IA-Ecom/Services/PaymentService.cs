using IA_Ecom.Models;
using IA_Ecom.Repositories;
using IA_Ecom.RequestModels;
using IA_Ecom.Services;

namespace IA_Ecom.Services
{
public class PaymentService(IPaymentRepository paymentRepository, IPaymentGateway paymentGateway) : IPaymentService
{
    public async Task<Payment> GetPaymentByOrderId(int orderId)
    {
        return await paymentRepository.GetPaymentByOrderId(orderId);
    }
    public async Task<bool> ProcessPayment(Order order)
    {
        try
        {
            // Validate order and payment details
            if (order == null || order.OrderItems == null || order.OrderItems.Count == 0)
            {
                throw new ArgumentException("Order is invalid.");
            }

            // Create payment request
            var paymentRequest = new PaymentRequest
            {
                Amount = order.TotalAmount,
                Currency = "USD",
                PaymentMethod = GetPaymentMethodForUser(order.CustomerId),
                Description = $"Order #{order.OrderId} payment"
            };

            // Process payment through the payment gateway
            var paymentResult = await paymentGateway.ChargeAsync(paymentRequest);

            if (paymentResult.IsSuccess)
            {
                // Update order payment status
                order.PaymentStatus = PaymentStatus.Paid;
                await paymentRepository.SavePaymentTransaction(order, paymentResult);
                return true;
            }
            else
            {
                // Log error or handle payment failure case
                Console.WriteLine($"Payment failed: {paymentResult.ErrorMessage}");
                return false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Payment processing error: {ex.Message}");
            return false;
        }
    }

    private PaymentMethod GetPaymentMethodForUser(string userId)
    {
        // Logic to retrieve user's payment method. For now this is dumy data
        return new PaymentMethod
        {
            CardNumber = "4111111111111111",
            ExpiryMonth = 12,
            ExpiryYear = 2024,
            Cvc = "123"
        };
    }

}
}
