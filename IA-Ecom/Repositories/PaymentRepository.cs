using IA_Ecom.Data;
using IA_Ecom.Models;
using IA_Ecom.Repositories;
using IA_Ecom.RequestModels;

public class PaymentRepository(ApplicationDbContext dbContext) : IPaymentRepository
{
    public async Task SavePaymentTransaction(Order order, PaymentResult paymentResult)
    {
        // Save the transaction details to the database
        var transaction = new PaymentTransaction
        {
            OrderId = order.OrderId,
            TransactionId = paymentResult.TransactionId,
            Amount = order.TotalAmount,
            PaymentDate = DateTime.UtcNow,
            Status = PaymentStatus.Paid
        };

        dbContext.PaymentTransactions.Add(transaction);
        await dbContext.SaveChangesAsync();
    }
    
}
