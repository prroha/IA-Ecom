using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        public ICollection<CartItem> CartItems { get; set; }

        public Cart()
        {
            CartItems = new List<CartItem>();
        }
    }
}