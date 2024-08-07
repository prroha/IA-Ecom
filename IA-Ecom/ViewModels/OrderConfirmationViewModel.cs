namespace IA_Ecom.ViewModels;

public class OrderConfirmationViewModel
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public List<OrderItemViewModel> OrderItems { get; set; }
    public decimal Subtotal { get; set; }
    public decimal ShippingCost { get; set; }
    public decimal TotalAmount { get; set; }
    public string ShippingAddress { get; set; }
    public string PaymentMethod { get; set; }
}