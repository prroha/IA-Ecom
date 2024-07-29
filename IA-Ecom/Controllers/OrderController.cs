using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // GET: /Order
        [Authorize(Roles = "ADMIN")] // Example of restricting access to admin role
        public IActionResult Index()
        {
            var orders = _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // GET: /Order/Details/{id}
        public IActionResult Details(int id)
        {
            var order = _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: /Order/Process/{id}
        [HttpPost]
        [Authorize(Roles = "ADMIN")] // Example of restricting access to admin role
        public IActionResult Process(int id)
        {
            var order = _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            // Process order logic (e.g., update order status, notify customer, etc.)

            return RedirectToAction(nameof(Index));
        }

        // GET: /Order/CartDetails
        public IActionResult CartDetails(string customerId)
        {
            var order = _orderService.GetCartItems(customerId);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        // POST: /ShoppingCart/AddToCart/{productId}
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // _orderService.AddToCart(productId);
            return RedirectToAction(nameof(Index));
        }
// POST: /Order/Checkout
        [HttpPost]
        [Authorize] // Require authentication to checkout
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Checkout(OrderViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get the user ID from identity
                if (string.IsNullOrEmpty(userId))
                {
                    return RedirectToAction("Login", "Account"); // Redirect to the login page if user ID is not found
                }
                // Example: Handle the complete order process in the service layer
                var success = await _orderService.ProcessOrderAsync(model.OrderId, userId);
                if (!success)
                {
                    return NotFound();
                }

                return RedirectToAction(nameof(Confirmation));
            }
            return View(model);
        }
        // GET: /Order/Confirmation
        public IActionResult Confirmation()
        {
            // Display order confirmation page
            return View();
        }

        // Other actions for managing orders
    }
}