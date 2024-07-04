using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }

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

        // Additional fields as needed for displaying or editing order details
    }

    public class OrderItemViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        // Additional fields as needed for order item details
    }
}