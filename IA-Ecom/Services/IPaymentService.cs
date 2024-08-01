using System.Collections.Generic;
using System.Threading.Tasks;
using IA_Ecom.Models;
using IA_Ecom.ViewModels;

namespace IA_Ecom.Services
{
    public interface IPaymentService
    {
        Task<bool> ProcessPayment(Order order);

    }
}
