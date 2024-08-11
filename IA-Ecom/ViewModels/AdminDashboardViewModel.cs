
namespace IA_Ecom.ViewModels;

public class AdminDashboardViewModel
{
    public string Title { get; set; }
    public int UsersCount { get; set; }
    public int ProductsCount { get; set; }
    public int OrdersCount { get; set; }
    public int FeedbacksCount { get; set; }
    public IEnumerable<UserViewModel> Users { get; set; }
    public IEnumerable<ProductViewModel> Products { get; set; }
    public IEnumerable<OrderViewModel> Orders { get; set; }
    public IEnumerable<FeedbackViewModel> Feedbacks { get; set; }
}
