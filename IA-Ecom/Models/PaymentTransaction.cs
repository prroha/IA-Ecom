using System.ComponentModel.DataAnnotations.Schema;
using IA_Ecom.RequestModels;

namespace IA_Ecom.Models;

public class PaymentTransaction: BaseModel
{
    public int PaymentTransactionId => Id;
    public int OrderId { get; set; }
    public string TransactionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime PaymentDate { get; set; }
    public PaymentStatus Status { get; set; }

        [ForeignKey("OrderId")]
    public Order Order { get; set; }
}