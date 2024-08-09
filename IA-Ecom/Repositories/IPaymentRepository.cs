using IA_Ecom.Models;
using IA_Ecom.RequestModels;

namespace IA_Ecom.Repositories
{
    public interface IPaymentRepository
    {
        Task<Payment> GetPaymentByOrderId(int orderId);
        Task SavePaymentTransaction(Order order, PaymentResult paymentResult);
    }
}
