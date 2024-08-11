using System.ComponentModel.DataAnnotations;
using IA_Ecom.Models;

namespace IA_Ecom.RequestModels;

public class PaymentMethod
{
    [Required(ErrorMessage = "Cardholder name is required.")]
    [StringLength(100, ErrorMessage = "Cardholder name cannot exceed 100 characters.")]
    public string CardHolderName { get; set; }
        
    [Required(ErrorMessage = "Card number is required.")]
    [CreditCard(ErrorMessage = "Invalid card number.")]
    public string CardNumber { get; set; }

    [Required(ErrorMessage = "Expiry month is required.")]
    [Range(1, 12, ErrorMessage = "Expiry month must be between 1 and 12.")]
    public int ExpiryMonth { get; set; }

    [Required(ErrorMessage = "Expiry year is required.")]
    [Range(2024, 2100, ErrorMessage = "Expiry year must be a valid year.")]
    public int ExpiryYear { get; set; }

    [Required(ErrorMessage = "CVV is required.")]
    [RegularExpression(@"\d{3,4}", ErrorMessage = "CVV must be 3 or 4 digits.")]
    public string CVV { get; set; }

    // [Required(ErrorMessage = "Payment method type is required.")]
    public string PaymentMethodType { get; set; }
    public string BillingAddress { get; set; }
    public string ZipCode { get; set; }
}

public enum PaymentStatus
{
    Pending,
    Paid,
    Failed,
    Refunded
}

public enum PaymentMethodType
{
    CreditCard,
    PayPal,
    BankTransfer,
}

public class PaymentMethodValidator
{
    public static bool ValidatePaymentMethod(PaymentMethod paymentMethod)
    {
        return ValidateCreditCard(paymentMethod);
        // switch (paymentMethod.PaymentMethodType)
        // {
        //     case PaymentMethodType.CreditCard:
        //         return ValidateCreditCard(paymentMethod);
        //     case PaymentMethodType.PayPal:
        //         return ValidatePayPal(paymentMethod);
        //     case PaymentMethodType.BankTransfer:
        //         return ValidateBankTransfer(paymentMethod);
        //     default:
        //         return false;
        // }
    }

    public static bool ValidateCreditCard(PaymentMethod paymentMethod)
    {
        if (string.IsNullOrWhiteSpace(paymentMethod.CardNumber) || !IsValidCardNumber(paymentMethod.CardNumber))
            return false;

// Check if the expiration date is in the past
        var currentDate = DateTime.Now;
        if (paymentMethod.ExpiryYear < currentDate.Year || 
            (paymentMethod.ExpiryYear == currentDate.Year && paymentMethod.ExpiryMonth < currentDate.Month))
            return false;

        if (string.IsNullOrWhiteSpace(paymentMethod.CVV) || paymentMethod.CVV.Length < 3 || paymentMethod.CVV.Length > 4)
            return false;

        return true;

    }

    private static bool IsValidCardNumber(string cardNumber)
    {
        return true;
        int sum = 0;
        bool alternate = false;

        for (int i = cardNumber.Length - 1; i >= 0; i--)
        {
            int n = int.Parse(cardNumber[i].ToString());
            if (alternate)
            {
                n *= 2;
                if (n > 9)
                {
                    n -= 9;
                }
            }

            sum += n;
            alternate = !alternate;
        }

        return (sum % 10 == 0);
    }

    // public static bool ValidatePayPal(PaymentMethod paymentMethod)
    // {
    //     if (string.IsNullOrWhiteSpace(paymentMethod.Email) || !IsValidEmail(paymentMethod.Email))
    //         return false;
    //
    //     // Further validation with PayPal API could be done here
    //     return true;
    // }

    public static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    // public static bool ValidateBankTransfer(PaymentMethod paymentMethod)
    // {
    //     if (string.IsNullOrWhiteSpace(paymentMethod.AccountNumber) || paymentMethod.AccountNumber.Length < 10)
    //         return false;
    //
    //     if (string.IsNullOrWhiteSpace(paymentMethod.BankName))
    //         return false;
    //
    //     // Further validation could include verifying the account with the bank's API
    //     return true;
    // }

    public static bool ValidatePaymentAmount(decimal amount)
    {
        return amount > 0;
    }
}