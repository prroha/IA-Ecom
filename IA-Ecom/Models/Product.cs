using System.Collections.Generic;

namespace IA_Ecom.Models
{
    public class Product: BaseModel
    {
        public int ProductId => Id;
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string Category { get; set; }
        public string? Color { get; set; }
        public string? ThumbnailImageUrl { get; set; }
        public DateTime EntryDate { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ProductImage> ProductImages { get; set; }

    }
}