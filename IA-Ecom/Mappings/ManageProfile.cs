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
        CreateMap<Order, OrderViewModel>();
        CreateMap<Product, ProductViewModel>()
            .ForMember(dest => dest.ImageUrls, opt => opt.MapFrom(src => 
                src.ProductImages != null ? src.ProductImages.Select(pi => pi.ImageUrl).ToList() : new List<string>()))
            .ForMember(dest => dest.ImageUrls, opt => opt.Ignore())
            .ReverseMap();    }
}