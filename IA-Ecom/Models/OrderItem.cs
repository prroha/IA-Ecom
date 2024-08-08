using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IA_Ecom.Models
{
    public class OrderItem: BaseModel
    {
        [Key]
        public int OrderItemId { get; set; }

        [Required]
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }
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