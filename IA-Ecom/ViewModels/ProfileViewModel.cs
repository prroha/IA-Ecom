using System.ComponentModel;

namespace IA_Ecom.ViewModels;

public class ProfileViewModel
{
    
    [ReadOnly(true)]
    public string UserId { get; set; }
    [ReadOnly(true)]
    public string Username { get; set; }
    public string? FullName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    [ReadOnly(true)]
    public string? ImageUrl { get; set; }

    [ReadOnly(true)]
    public string Email { get; set; }
    public IFormFile? ImageInput { get; set; }    
}