namespace ShoppingApp.ServiceInterfaces
{
    using ShoppingApp.Model;
    using ShoppingApp.Model.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IOrderPaymentDetails
    {
        Task<List<OrderAndPaymentResponse>> GetOrderPaymentDetails(string userId);
        Task<bool> AddOrderPaymentDetails(OrderAndPaymentRequest orderPaymentrequest);
    }
}
