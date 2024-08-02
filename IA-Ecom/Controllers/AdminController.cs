using System.Collections;
using IA_Ecom.Mappers;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.Models;
using IA_Ecom.Services;
using IA_Ecom.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace IA_Ecom.Controllers
{
    [Route("admin")]
    [Authorize(Roles = "ADMIN")]
    public class AdminController(
        IProductService productService,
        IOrderService orderService,
        IUserService userService,
    UserManager<User> userManager,
        IFeedbackService feedbackService)
        : Controller
    {
        public IActionResult Index()
        {
                return RedirectToAction(nameof(Dashboard));
        }
        
// GET: /Admin/Dashboard
        [HttpGet]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> Dashboard()
        {
            var usersCount = await userService.CountAllAsync();
            var productsCount = await productService.CountAllAsync();
            var ordersCount = await orderService.CountAllAsync();

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
        
        [HttpPost("profile")]
        public async Task<IActionResult> Profile()
        {
            var profile = new ProfileViewModel();
            if (ModelState.IsValid)
            {
                // await _productService.AddProductAsync(product);
                // return RedirectToAction(nameof(Products));
            }
            return View(profile);
        }
        // GET: /admin/products
        [HttpGet("products")]
        public async Task<IActionResult> ManageProducts()
        {
            IEnumerable<Product> products = await productService.GetAllProductsAsync();
            List<ProductViewModel> productsViewModel = products.Select(o => ProductMapper.MapToViewModel(o)).ToList();
            ManageProductViewModel viewModel = new ManageProductViewModel();
            viewModel.Products = productsViewModel;
            return View(viewModel);
        }

        // GET: /admin/products/{id}
        [HttpGet("products/{id}")]
        public async Task<IActionResult> ProductDetails(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost("products")]
        public async Task<IActionResult> AddProduct(ProductViewModel productModel)
        {
            if (ModelState.IsValid)
            {
                Product product = ProductMapper.MapToModel(productModel);
                product.EntryDate = DateTime.Now;
                await productService.AddProductAsync(product);
                return RedirectToAction(nameof(ManageProducts));
            }
            return View(productModel);
        }

        // GET: /admin/orders
        [HttpGet("orders")]
        public async Task<IActionResult> ManageOrders()
        {
            IEnumerable<Order> orders = await orderService.GetAllOrdersAsync();
            List<OrderViewModel> orderViewModel = orders.Select(o => OrderMapper.MapToViewModel(o)).ToList();
            ManageOrderViewModel viewModel = new ManageOrderViewModel();
            viewModel.Orders = orderViewModel;
            return View(viewModel);
        }

        // GET: /admin/orders/{id}
        [HttpGet("orders/{id}")]
        public async Task<IActionResult> OrderDetails(int id)
        {
            var order = await orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: /admin/users
        [HttpGet("users")]
        public async Task<IActionResult> ManageUsers()
        {
            var users = await userService.GetAllUsersAsync(); // Replace with your service method
            var model = new List<UserViewModel>();

            foreach (var user in users)
            {
                var roles = await userManager.GetRolesAsync(user);
                var userViewModel = new UserViewModel
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = roles.FirstOrDefault() // Assuming you have a method to fetch user's role
                };
                model.Add(userViewModel);
            }

            return View(model);
        }

        // GET: /admin/users/{id}
        [HttpGet("users/{id}")]
        public async Task<IActionResult> UserDetails(int id)
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpGet("feedbacks")]
        public async Task<IActionResult> ManageFeedbacks()
        {
            var feedbacks = await feedbackService.GetAllFeedbacksAsync();
            List<FeedbackViewModel> viewModel = feedbacks.Select(o => FeedbackMapper.MapToViewModel(o)).ToList();
            ManageFeedbackViewModel feedbackViewModel = new ManageFeedbackViewModel();
             feedbackViewModel.Feedbacks = viewModel;
            return View(feedbackViewModel);
        }

        // GET: /admin/feedbacks/{id}
        [HttpGet("feedbacks/{id}")]
        public async Task<IActionResult> FeedbackDetails(int id)
        {
            var feedback = await feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound();
            }
            return View(feedback);
        }

        // Other admin actions for managing users, orders, and feedbacks.
    }
}
