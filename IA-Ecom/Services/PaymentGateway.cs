using IA_Ecom.RequestModels;

namespace IA_Ecom.Services;

public class PaymentGateway: IPaymentGateway
{
        public async Task<PaymentResult> ChargeAsync(PaymentRequest request)
        {
            // Simulating calling a payment API.  Simulating network delay
            await Task.Delay(1000); 

            // Simulating payment success
            bool paymentSuccess = true; 

            if (paymentSuccess)
            {
                return new PaymentResult
                {
                    IsSuccess = true,
                    TransactionId = Guid.NewGuid().ToString(),
                    ErrorMessage = string.Empty
                };
            }

            return new PaymentResult
            {
                IsSuccess = false,
                TransactionId = string.Empty,
                ErrorMessage = "Payment failed due to insufficient funds."
            };
        }
    }