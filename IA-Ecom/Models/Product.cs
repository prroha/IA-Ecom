using System.Collections.Generic;

namespace IA_Ecom.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
    }
}