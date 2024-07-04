using System.Collections.Generic;
namespace IA_Ecom.ViewModels;

public class CartViewModel
{
        public List<CartItemViewModel> CartItems { get; set; }

        public decimal TotalPrice { get; set; }

        // Additional properties as needed for displaying shopping cart details

}
    public class CartItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Additional fields as needed for cart item details
    }
