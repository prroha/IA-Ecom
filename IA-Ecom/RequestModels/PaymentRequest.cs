namespace IA_Ecom.RequestModels;

public class PaymentRequest
{
    public decimal Amount { get; set; }
    public string Currency { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public string Description { get; set; }
    public string CustomerEmail { get; set; }
}

public class PaymentResult
{
    public bool IsSuccess { get; set; }
    public string TransactionId { get; set; }
    public string ErrorMessage { get; set; }
    
    // Additional fields
    public string Status { get; set; }
    public DateTime PaymentDate { get; set; }
}
