namespace ShoppingApp.Services.IServices
{
    using ShoppingApp.Models.Model;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderPaymentDetails
    {
        Task<OrderAndPaymentResponse> GetOrderPaymentDetails(string userId);
        Task<bool> AddOrderPaymentDetails(OrderAndPaymentRequest orderPaymentrequest);
    }
}
