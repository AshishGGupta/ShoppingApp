namespace ShoppingApp.Models.Model
{
    using ShoppingApp.Models.Domain;
    using System;
    using System.Collections.Generic;

    public class OrderAndPaymentResponse
    {
        public int OrderId { get; set; }

        public Guid OrderToken { get; set; }

        public string TokenUserId { get; set; }

        public List<ProductDetailsForOrder> productDetailsForOrders { get; set; }

        public UserDetails UserDetail { get; set; }

        public int TotalQuantity { get; set; }

        public string PaymentType { get; set; }

        public DateTime OrderDate { get; set; }
    }

    public class ProductDetailsForOrder
    {
        public Product Products { get; set; }

        public int ProductQuantity { get; set; }
    }
}
