using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.Models
{
    public class Order
    {
        public User Customer { get; set; }

        [Key]
        public int OrderId { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }


        public decimal TotalAmount { get; set; }

        // Additional fields for shipping information
        public string ShippingAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
