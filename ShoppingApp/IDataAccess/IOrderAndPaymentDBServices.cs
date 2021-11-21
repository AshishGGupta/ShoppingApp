using ShoppingApp.Model.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApp.IDataAccess
{
    public interface IOrderAndPaymentDBServices
    {
        Task<List<OrderAndPayment>> GetOrderAndPaymentDetails(string userId);
        Task AddOrderAndPaymentDetails(OrderAndPayment orderAndPaymentDetail);
    }
}
