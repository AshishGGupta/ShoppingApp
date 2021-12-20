namespace ShoppingApp.Services.Services
{
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Common;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Services.IServices;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System;

    public class OrderPaymentDetails : IOrderPaymentDetails
    {
        private readonly IDbFacade _dbCollection;
        private readonly ILogger<OrderPaymentDetails> _logger;
        private readonly Mapper _mapper;

        public OrderPaymentDetails(IDbFacade dbFacade, ILogger<OrderPaymentDetails> logger)
        {
            _dbCollection = dbFacade;
            _logger = logger;
            _mapper = new Mapper();
        }

        public async Task<bool> AddOrderPaymentDetails(OrderAndPaymentRequest orderPaymentrequest)
        {
            var cartList = await _dbCollection.CartDbService.GetCartDetails(orderPaymentrequest.TokenUserId);
            if(cartList?.Count > 0 && cartList.Any(x => x.Product.ProductQuantity > 0))
            {
                var orderAndPaymentDetail = _mapper.MapOrderAndPaymentDetail(orderPaymentrequest, cartList);
                await _dbCollection.OrderDBServices.AddOrderAndPaymentDetails(orderAndPaymentDetail);
                await _dbCollection.CartDbService.BulkCartDelete(cartList);
                cartList.ForEach(x => x.Product.ProductQuantity = x.Product.ProductQuantity - Convert.ToInt32(x.Quantity));
                foreach (var product in cartList.Select(x => x.Product))
                {
                    await _dbCollection.ProductDbServices.UpdateProduct(product);
                }
                _logger.LogInformation("Order Placed Successfully. userId:" + orderPaymentrequest.TokenUserId);
                return true;
            }
            _logger.LogInformation("No Cart detials found Or Product is not available currently" + orderPaymentrequest.TokenUserId);
            return false;
        }

        public async Task<OrderAndPaymentResponse> GetOrderPaymentDetails(string userId)
        {
            var orderList = await _dbCollection.OrderDBServices.GetOrderAndPaymentDetails(userId);
            if(orderList?.Count > 0)
            {
                var response = _mapper.MapOrderPaymentResponse(orderList);
                _logger.LogInformation($"Order details found. order count={response.productDetailsForOrders.Count}, userId={userId}");
                return response;
            }
            _logger.LogInformation(" No order history found.userId: " + userId);
            return null;
        }
    }
}
