namespace ShoppingApp.DataAccess.IDataAccess
{
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderAndPaymentDBServices
    {
        Task<List<OrderAndPayment>> GetOrderAndPaymentDetails(string userId);
        Task AddOrderAndPaymentDetails(List<OrderAndPayment> orderAndPaymentDetail);
    }
}
