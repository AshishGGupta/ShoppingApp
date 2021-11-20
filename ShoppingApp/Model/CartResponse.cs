namespace ShoppingApp.Model
{
    using ShoppingApp.Model.Domain;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CartResponse
    {
        public List<ProductQuantity> ProductQuantity { get; set; }
        
        public Decimal TotalPrice { get; set; } 

        public List<UserDetails> UserDetails { get; set; }

        public string Message { get; set; }

        public CartResponse(List<ProductQuantity> product, List<UserDetails> userDetails, string msg)
        {
            ProductQuantity = product; 
            UserDetails = userDetails;
            Message = msg;
            TotalPrice = product?.Count > 0 ? product.Sum(x => x.Product.Price * x.Quantity) : 0;
        }
    }
}
