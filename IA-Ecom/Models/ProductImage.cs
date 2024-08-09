using System.ComponentModel.DataAnnotations.Schema;

namespace IA_Ecom.Models;

public class ProductImage: BaseModel
{
    public int ProductImageId => Id;
    public string ImageName { get; set; }
    public string ImageUrl { get; set; }
    public int ProductId { get; set; }

    // Navigation property to the product
        [ForeignKey("ProductId")]
    public Product Product { get; set; }
}