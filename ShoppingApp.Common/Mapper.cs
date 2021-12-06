namespace ShoppingApp.Common
{
    using Newtonsoft.Json;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Mapper
    {
        public List<OrderAndPayment> MapOrderAndPaymentDetail(OrderAndPaymentRequest orderPaymentrequest, List<Cart> cartList)
        {
            Guid orderToken = Guid.NewGuid();
            var date = DateTime.Now;
            return cartList.Select(x => new OrderAndPayment()
            {
                OrderToken = orderToken,
                PaymentType = orderPaymentrequest.PaymentType,
                TokenUserId = orderPaymentrequest.TokenUserId,
                UserDetailsId = orderPaymentrequest.UserDetailsId,
                Quantity = cartList?.Count ?? 0,
                ProductId = x.ProductId,
                ProductQuantity = cartList.Where(y => y.ProductId == x.ProductId).Select(y => Convert.ToInt32(y.Quantity)).FirstOrDefault(),
                OrderDate = date
            }).ToList();
        }

        public OrderAndPaymentResponse MapOrderPaymentResponse(List<OrderAndPayment> orderAndPayments)
        {
            var productDetailsForOrder = orderAndPayments.Select(x => new ProductDetailsForOrder()
            {
                Products = x.Product,
                ProductQuantity = x.ProductQuantity
            }).ToList();
            return orderAndPayments.Select(x => new OrderAndPaymentResponse()
            {
                OrderId = x.OrderId,
                OrderToken = x.OrderToken,
                OrderDate = x.OrderDate,
                PaymentType = x.PaymentType,
                TokenUserId = x.TokenUserId,
                TotalQuantity = x.Quantity,
                UserDetail = x.UserDetail,
                productDetailsForOrders = productDetailsForOrder
            }).FirstOrDefault();
        }
    }
}
