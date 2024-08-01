using System.Security.Claims;
using AutoMapper;
using IA_Ecom.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Controllers
{
    public class OrderController(IOrderService orderService, Mapper mapper) : Controller
    {
        private readonly IMapper _mapper = mapper;

        [Authorize(Roles = "ADMIN")] 
        public IActionResult Index()
        {
            var orders = orderService.GetAllOrdersAsync();
            return View(orders);
        }

        public IActionResult Details(int id)
        {
            var order = orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")] 
        public IActionResult Process(int id)
        {
            var order = orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Process order logic (e.g., update order status, notify customer, etc.)

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetCartDetails()
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cart = await orderService.GetCartDetailsAsync(customerId);

            if (cart == null)
            {
                // return View("EmptyCart");
                return NotFound();
            }

            return View("CartDetails", cart);
        }
        
        [HttpPost]
        [Authorize] 
        public async Task<IActionResult> AddToCartAsync(OrderItemViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID from identity
                OrderItem orderItem = OrderMapper.MapToModel(viewModel);
                orderItem.CustomerId = userId;
                await orderService.AddToCartAsync(orderItem);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }
        
        [HttpPost]
        [Authorize] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); // Redirect to the login page if user ID is not found
            }
            var success = await orderService.CheckoutAsync(userId);

            if (!success)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(OrderConfirmation));
        }

        public IActionResult OrderConfirmation()
        {
            return View();
        }

    }
}