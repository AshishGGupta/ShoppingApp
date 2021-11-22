namespace ShoppingApp.Services.IServices
{
    using ShoppingApp.Models.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderPaymentDetails
    {
        Task<List<OrderAndPaymentResponse>> GetOrderPaymentDetails(string userId);
        Task<bool> AddOrderPaymentDetails(OrderAndPaymentRequest orderPaymentrequest);
    }
}
