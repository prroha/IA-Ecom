using System.ComponentModel.DataAnnotations;
using IA_Ecom.Models;

namespace IA_Ecom.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; } = 0;

        [Required]
        public string Name { get; set; }
        [Required]
        public string Category { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public string Description { get; set; }
        
        public string? ThumbnailImageUrl { get; set; }
        public string? SKU { get; set; }
        public string? Color { get; set; }
        public string? Size { get; set; }
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
        // For multiple image uploads
        public List<IFormFile>? ImagesInput { get; set; }    
        public List<string> ImageUrls { get; set; } = new(); 
    }
}