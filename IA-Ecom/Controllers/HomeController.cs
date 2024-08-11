using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IA_Ecom.Controllers;


public class HomeController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;

    public HomeController(ILogger<HomeController> logger, IProductService productService,IMapper mapper)
    {
        _logger = logger;
        _productService = productService;
        _mapper = mapper;
    }

    public async Task<IActionResult> Index()
    {
        if (User.Identity.IsAuthenticated && User.IsInRole("ADMIN"))
        {
            return RedirectToAction("Dashboard", "Admin");
        }
        var products = await _productService.GetAllProductsAsync();
        List<ProductViewModel> viewModel = products
            .OrderByDescending(p => p.EntryDate) // Order by EntryDate descending
            .Take(6)
            .Select(p => _mapper.Map<ProductViewModel>(p))
            .ToList();

        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult ContactUs()
    {
        return View();
    }
    public IActionResult About()
    {
        return View();
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}