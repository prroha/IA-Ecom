using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }

        // Other fields as needed for displaying or editing product details
    }
}