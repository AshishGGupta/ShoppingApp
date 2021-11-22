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
        public OrderAndPayment MapOrderAndPaymentDetail(OrderAndPaymentRequest orderPaymentrequest, List<Cart>cartList)
        {
            return new OrderAndPayment()
            {
                PaymentType = orderPaymentrequest.PaymentType,
                TokenUserId = orderPaymentrequest.TokenUserId,
                UserDetailsId = orderPaymentrequest.UserDetailsId,
                Quantity = cartList?.Count ?? 0,
                ProductId = JsonConvert.SerializeObject(cartList.Select(x => x.ProductId).ToList()),
                OrderDate = DateTime.Now
            };
        }

        public List<OrderAndPaymentResponse> MapOrderPaymentResponse(List<OrderAndPayment> orderAndPayments, List<Product> products)
        {
            return orderAndPayments.Select(x => new OrderAndPaymentResponse()
            {
                OrderId = x.OrderId,
                OrderDate = x.OrderDate,
                PaymentType = x.PaymentType,
                TokenUserId = x.TokenUserId,
                TotalQuantity = x.Quantity,
                UserDetail = x.UserDetail,
                Products = products.Where(y => (x.ProductId.Substring(1, x.ProductId.Length - 2).Split(',')).ToList().Contains(y.ProductId.ToString())).ToList()
            }).ToList();
        }
    }
}
