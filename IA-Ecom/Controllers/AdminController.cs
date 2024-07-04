using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace IA_Ecom.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IProductService _productService;
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IFeedbackService _feedbackService;

        public AdminController(
            IProductService productService,
            IOrderService orderService,
            IUserService userService,
            IFeedbackService feedbackService)
        {
            _productService = productService;
            _orderService = orderService;
            _userService = userService;
            _feedbackService = feedbackService;
        }

        public IActionResult Index()
        {
                return RedirectToAction(nameof(Dashboard));
        }
        
// GET: /Admin/Dashboard
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Dashboard()
        {
            var usersCount = await _userService.CountAllAsync();
            var productsCount = await _productService.CountAllAsync();
            var ordersCount = await _orderService.CountAllAsync();

            var dashboardData = new AdminDashboardViewModel
            {
                UsersCount = usersCount,
                ProductsCount = productsCount,
                OrdersCount = ordersCount,
               Users = [],
               Products = [],
               Orders = [],
               Feedbacks = [],
            };

            // Example: Mapping ApplicationUser to AdminDashboardViewModel
            // Replace with actual logic based on your application's requirements
            // var users = await _dbContext.Users.ToListAsync();
            // var mappedUsers = _mapper.Map<List<User>, List<AdminDashboardViewModel>>(users);

            return View(dashboardData);
        }
        // GET: /admin/products
        [HttpGet("products")]
        public async Task<IActionResult> Products()
        {
            var products = await _productService.GetAllProductsAsync();
            return View(products);
        }

        // GET: /admin/products/{id}
        [HttpGet("products/{id}")]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: /admin/products/add
        [HttpPost("products/add")]
        public async Task<IActionResult> AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                await _productService.AddProductAsync(product);
                return RedirectToAction(nameof(Products));
            }
            return View(product);
        }

        // GET: /admin/orders
        [HttpGet("orders")]
        public async Task<IActionResult> Orders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }

        // GET: /admin/orders/{id}
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: /admin/users
        [HttpGet("users")]
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetAllUsersAsync();
            return View(users);
        }

        // GET: /admin/users/{id}
        [HttpGet("users/{id}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // GET: /admin/feedbacks
        [HttpGet("feedbacks")]
        public async Task<IActionResult> Feedbacks()
        {
            var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
            return View(feedbacks);
        }

        // GET: /admin/feedbacks/{id}
        [HttpGet("feedbacks/{id}")]
        public async Task<IActionResult> FeedbackDetails(int id)
        {
            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // Other admin actions for managing users, orders, and feedbacks.
    }
}
