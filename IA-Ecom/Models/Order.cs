using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using IA_Ecom.RequestModels;

namespace IA_Ecom.Models
{
    public class Order: BaseModel
    {

        public int OrderId => Id;

        [Required]
        public string CustomerId { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        public string Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public decimal TotalAmount { get; set; }

        // Additional fields for shipping information
        public string ShippingAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}
