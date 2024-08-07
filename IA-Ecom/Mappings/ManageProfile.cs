using AutoMapper;
using IA_Ecom.Models;
using IA_Ecom.ViewModels;
using IA_Ecom.Models;
using IA_Ecom.ViewModels;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<User, UserViewModel>();
        // For Product
        CreateMap<Product, ProductViewModel>();
        CreateMap<Order, OrderViewModel>();

    }
}