using IA_Ecom.Models;

namespace IA_Ecom.Services;

public class AdminService: IAdminService
{
    private readonly IProductService _productService;

    public AdminService(IProductService productService)
    {
        _productService = productService;
    }

}