namespace ShoppingApp.Models.Model
{
    using ShoppingApp.Models.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CartResponse
    {
        public List<ProductQuantity> ProductQuantity { get; set; }
        
        public Decimal TotalPrice { get; set; } 

        public List<UserDetails> UserDetails { get; set; }

        public string Message { get; set; }

        public bool DeliveryChange { get; set; } = false;

        public CartResponse(List<ProductQuantity> product, List<UserDetails> userDetails, string msg)
        {
            ProductQuantity = product; 
            UserDetails = userDetails;
            Message = msg;
            TotalPrice = product?.Count > 0 ? product.Sum(x => x.Product.Price * x.Quantity) : 0;
            if (TotalPrice <= 500)
            {
                DeliveryChange = true;
                TotalPrice += 50;
            }
        }
    }
}
