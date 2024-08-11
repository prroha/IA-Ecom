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
    public async Task<bool> ProcessPayment(Order order, PaymentMethod paymentMethod)
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
                PaymentMethod = GetPaymentMethodForUser(paymentMethod),
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
                // Log error or handle payment failure case
                Console.WriteLine($"Payment failed: {paymentResult.ErrorMessage}");
                return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Payment processing error: {ex.Message}");
            return false;
        }
    }

    private PaymentMethod GetPaymentMethodForUser(PaymentMethod paymentMethod)
    {
        return new PaymentMethod
        {
            CardNumber = paymentMethod.CardNumber,
            ExpiryMonth = paymentMethod.ExpiryMonth,
            ExpiryYear = paymentMethod.ExpiryYear,
            CVV = paymentMethod.CVV
        };
    }

}
}
