using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using IA_Ecom.Mappers;
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

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllProductsAsync();
            List<ProductViewModel> viewModel = products.Select(p => _mapper.Map<ProductViewModel>(p)).ToList();
            return View("Catalog", viewModel);
        }

        public async Task<IActionResult> Details(int id)
        {
            Product product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            // Map Product to ProductViewModel
            // var productViewModel = _mapper.Map<ProductViewModel>(product); 
            ProductViewModel productViewModel = ProductMapper.MapToViewModel(product);

            return View(productViewModel);
        }
        
    }
}