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
            UserId = user.Id,
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
    public static User MapToModel(User user, UserViewModel viewModel)
    {
           user.Id = viewModel.UserId;
           user.FirstName = viewModel.FirstName;
           user.LastName = viewModel.LastName;
           user.PhoneNumber = viewModel.PhoneNumber;
           user.Address = viewModel.Address;
           user.Email = viewModel.Email;
           return user;
    }
    public static User MapToModel(ProfileViewModel viewModel)
    {
        return new User
        {
            Id = viewModel.UserId,
            FirstName = viewModel.FirstName,
            LastName = viewModel.LastName,
            PhoneNumber = viewModel.PhoneNumber,
            Address = viewModel.Address,
            Email = viewModel.Email,
        };
    }
    public static User MapToModel(User user, ProfileViewModel viewModel)
    {
           user.Id = viewModel.UserId;
           user.FirstName = viewModel.FirstName;
           user.LastName = viewModel.LastName;
           user.PhoneNumber = viewModel.PhoneNumber;
           user.Address = viewModel.Address;
           user.Email = viewModel.Email;
            return user;
    }
    public static ProfileViewModel MapToProfileViewModel(User user)
    {
        return new ProfileViewModel
        {
            UserId = user.Id,
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