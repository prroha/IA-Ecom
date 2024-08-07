using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Mappers;

public static class UserMapper
{
    public static UserViewModel MapToViewModel(User user)
    {
        return new UserViewModel
        {
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
        };
    }
}