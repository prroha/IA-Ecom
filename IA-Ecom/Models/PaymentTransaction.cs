using IA_Ecom.RequestModels;

namespace IA_Ecom.Models;

public class PaymentTransaction
{
    public int PaymentTransactionId { get; set; }
    public int OrderId { get; set; }
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }

    public Order Order { get; set; }
}