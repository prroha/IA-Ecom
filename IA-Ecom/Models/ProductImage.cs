namespace IA_Ecom.Models;

public class ProductImage
{
    public int ProductImageId { get; set; }
    public string ImageUrl { get; set; }
    public int ProductId { get; set; }

    // Navigation property to the product
    public Product Product { get; set; }
}