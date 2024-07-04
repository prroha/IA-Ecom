using System.ComponentModel.DataAnnotations;

namespace IA_Ecom.ViewModels
{
    public class PaymentViewModel
    {
        [Required]
        [Display(Name = "Cardholder Name")]
        public string CardholderName { get; set; }

        [Required]
        [DataType(DataType.CreditCard)]
        [Display(Name = "Credit Card Number")]
        public string CreditCardNumber { get; set; }

        [Required]
        [Display(Name = "Expiration Month")]
        public int ExpirationMonth { get; set; }

        [Required]
        [Display(Name = "Expiration Year")]
        public int ExpirationYear { get; set; }

        [Required]
        [Display(Name = "CVV")]
        public string CVV { get; set; }

        // Additional fields as needed for payment details
    }
}