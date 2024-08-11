using Microsoft.AspNetCore.Mvc.Rendering;

namespace IA_Ecom.ViewModels;

public class UserViewModel
{
    public string UserId { get; set; }
    public string? Username { get; set; }
    public string Email { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? FullName { get; set; }
    public string Role { get; set; } = "USER";
    public string? Address { get; set; }
    public string? PhoneNumber { get; set; }    
    public List<SelectListItem> RoleOptions { get; set; } = new()
    {
        new SelectListItem { Value = "ADMIN", Text = "Admin" },
        new SelectListItem { Value = "USER", Text = "User" }
    };
}