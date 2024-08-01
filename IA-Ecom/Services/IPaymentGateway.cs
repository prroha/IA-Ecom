using IA_Ecom.RequestModels;

namespace IA_Ecom.Services;

public interface IPaymentGateway
{
    Task<PaymentResult> ChargeAsync(PaymentRequest request);

}