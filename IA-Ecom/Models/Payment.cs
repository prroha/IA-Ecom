using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IA_Ecom.Models
{
    public class Payment: BaseModel
    {
        public int PaymentId => Id;

        [Required]
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime PaymentDate { get; set; }

        public string TransactionId { get; set; }

        // Additional fields for payment status
        public string Status { get; set; }

        // Additional fields for payment gateway response
        public string GatewayResponse { get; set; }
    }
}