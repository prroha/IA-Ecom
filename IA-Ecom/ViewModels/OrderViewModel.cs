using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.ViewModels
{
    public class OrderViewModel
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public string CustomerId { get; set; }
        [Required]
        public string CustomerName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string CustomerEmail { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string CustomerPhone { get; set; }

        public DateTime OrderDate { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }

        public string Status { get; set; }
    }

    public class OrderItemViewModel
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        
        public string ProductSize { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}