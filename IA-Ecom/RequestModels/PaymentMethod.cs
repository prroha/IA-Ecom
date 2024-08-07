using IA_Ecom.Models;

namespace IA_Ecom.RequestModels;

public class PaymentMethod
{
        public string CardNumber { get; set; }
        public int ExpiryMonth { get; set; }
        public int ExpiryYear { get; set; }
        public string Cvc { get; set; }

        public string CardHolderName { get; set; }
        public string BillingAddress { get; set; }
        public string ZipCode { get; set; }
    }

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed,
    Refunded
}
