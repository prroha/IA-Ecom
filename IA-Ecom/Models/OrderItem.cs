using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IA_Ecom.Models
{
    public class OrderItem: BaseModel
    {
        public int OrderItemId => Id;

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public string ProductSize { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
        public string CustomerId { get; set; } // Assuming you have user-specific carts


        public decimal TotalPrice => Quantity * UnitPrice;
    }
}