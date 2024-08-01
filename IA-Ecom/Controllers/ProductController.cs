using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // GET: /Product
        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            List<ProductViewModel> viewModel = products.Select(p => _mapper.Map<ProductViewModel>(p)).ToList();
            return View("Catalog", viewModel);
        }

        // GET: /Product/Details/{id}
        public IActionResult Details(int id)
        {
            var product = _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            var productViewModel = _mapper.Map<ProductViewModel>(product); // Map Product to ProductViewModel

            return View(productViewModel);
        }

        // Other actions for managing products
    }
}