namespace IA_Ecom.ViewModels;

using System.ComponentModel.DataAnnotations;
public class ContactUsViewModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Subject is required.")]
    [StringLength(150, ErrorMessage = "Subject cannot be longer than 150 characters.")]
    public string Subject { get; set; }

    [Required(ErrorMessage = "Message is required.")]
    [StringLength(1000, ErrorMessage = "Message cannot be longer than 1000 characters.")]
    public string Message { get; set; }

    public bool IsUrgent { get; set; } // Optional 
}
