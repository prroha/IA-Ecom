using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
        
        public string ImageUrl { get; set; }
        public string ProductCondition { 
            get
            {
                TimeSpan timeSinceEntry = DateTime.Now - EntryDate;
                if (timeSinceEntry.Days <= 30) return "NEW";
                return string.Empty;
            }
        }
        public int Stock { get; set; }

        public DateTime EntryDate { get; set; }
        // Other fields as needed for displaying or editing product details
    }
}