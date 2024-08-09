using System.Threading.Tasks;
using IA_Ecom.Controllers;
using IA_Ecom.Mappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using IA_Ecom.ViewModels; // Ensure to include your ViewModel namespace
using IA_Ecom.Models;
using IA_Ecom.Services; // Ensure to include your ApplicationUser class namespace

namespace IA_Ecom.Controllers;

public class AccountController(
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    RoleManager<IdentityRole> roleManager,
    IUserService userService,
    INotificationService notificationService)
    : Controller
{
    // GET: /Account/Login
    [HttpGet]
    public IActionResult Login(string returnUrl = null)
    {
        if (User.Identity.IsAuthenticated)
        {
            // Check if the user is in the Admin role
            if (User.IsInRole("ADMIN"))
            {
                return RedirectToAction("Dashboard", "Admin"); // Redirect to Admin Dashboard
            }
            else
            {
                return RedirectToAction("Index", "Home"); // Redirect to Home Page for regular users
            }
        }
        else
        {
            ViewData["ReturnUrl"] = returnUrl;
            var model = new LoginViewModel();
            return View(model);
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;

        if (ModelState.IsValid)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user != null)
            {
                var result = await signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    if (roles.Contains("ADMIN"))
                    {
                        return RedirectToAction("Dashboard", "Admin");
                    }
                    else if (roles.Contains("USER"))
                    {
                        return RedirectToLocal(returnUrl);
                    }
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");
        }

        // If model state is not valid or login fails, redisplay the form with validation errors
        return View(model);
    }
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = new User { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName};
            var result = await userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                if (await roleManager.RoleExistsAsync(model.Role))
                {
                    await userManager.AddToRoleAsync(user, model.Role);
                }
                await signInManager.SignInAsync(user, isPersistent: false);
                var roles = await userManager.GetRolesAsync(user);
                if (roles.Contains("ADMIN"))
                {
                    return RedirectToAction("Dashboard", "Admin");
                }
                else if (roles.Contains("USER"))
                {
                    return RedirectToLocal(returnUrl);
                }
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }
    
    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await signInManager.SignOutAsync();
        // return RedirectToAction("Index", "Home");
        return RedirectToLocal();
    }
    
    private IActionResult RedirectToLocal(string returnUrl = "")
    {
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        else
        {
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetProfile(string userId)
    {
        var user = await userService.GetUserByUserIdAsync(userId);
        if (user == null)
        {
            notificationService.AddNotification("User Profile Not Found", NotificationType.Error);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        ProfileViewModel viewModel = UserMapper.MapToProfileViewModel(user);
        return View("../Admin/Profile", viewModel); 
    }
    [HttpPost]
    public async Task<IActionResult> Profile(ProfileViewModel viewModel)
    {
        if (ModelState.IsValid &&  !String.IsNullOrEmpty(viewModel.UserId))
        {
            var existingUser = await userService.GetUserByUserIdAsync(viewModel.UserId);
            if (existingUser == null)
            {
                notificationService.AddNotification("User Not Found", NotificationType.Error);
                return Redirect(Request.Headers["Referer"].ToString());
            }
            User user = UserMapper.MapToModel(viewModel);
            user.UpdatedDate = DateTime.UtcNow;
            notificationService.AddNotification("Profile Updated Successfully", NotificationType.Success);
            await userService.UpdateProfileAsync(user, viewModel.ImageInput);
            return RedirectToAction(nameof(GetProfile), new{useriD= viewModel.UserId}); 
        }
        notificationService.AddNotification("Error Occurred", NotificationType.Validation);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}