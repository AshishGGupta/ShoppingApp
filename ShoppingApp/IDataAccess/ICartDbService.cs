﻿namespace ShoppingApp.IDataAccess
{
    using ShoppingApp.Model.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartDbService
    {
        Task<List<Cart>> GetCartDetails(string userId);
        Task AddToCart(Cart cart);
        Task Edit(Cart cart);
        Task Delete(Cart cart);
        Task<Cart> CartItemExists(int cartId);
        Task<Cart> CartItemExistsByProductId(int productId);
    }
}
