using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Mappers;

public static class UserMapper
{
    public static UserViewModel MapToViewModel(User user)
    {
        return new UserViewModel
        {
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
        };
    }
    public static User MapToModel(UserViewModel viewModel)
    {
        return new User
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            PhoneNumber = viewModel.PhoneNumber,
            Address = viewModel.Address,
            Email = viewModel.Email,
        };
    }
    public static User MapToModel(ProfileViewModel viewModel)
    {
        return new User
        {
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            PhoneNumber = viewModel.PhoneNumber,
            Address = viewModel.Address,
            Email = viewModel.Email,
        };
    }
    public static ProfileViewModel MapToProfileViewModel(User user)
    {
        return new ProfileViewModel
        {
            Username = user.UserName,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Address = user.Address,
            FullName = $"{user.FirstName} {user.LastName}",
            Email = user.Email,
            ImageUrl = user.ImageUrl,
        };
    }
}