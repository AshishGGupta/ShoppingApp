namespace ShoppingApp.Services.Services
{
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Threading.Tasks;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using System;
    using System.Collections.Generic;

    public class CartServices : ICartServices
    {
        private readonly IDbFacade _dbCollection;
        private readonly ILogger<CartServices> _logger;

        public CartServices(IDbFacade dbFacade, ILogger<CartServices> logger)
        {
            _dbCollection = dbFacade;
            _logger = logger;
        }

        public async Task<CartResponse> GetCartDetails(string userId)
        {
            string msg = "";
            List<Cart> cartList = await _dbCollection.CartDbService.GetCartDetails(userId);
            List<ProductQuantity> listProductQuantity = cartList?.Select(x => new ProductQuantity()
                                                        {
                                                            CartId = x.CartId,
                                                            Product = x.Product,
                                                            Quantity = Convert.ToInt32(x.Quantity),
                                                            ProductExpired = (!string.IsNullOrEmpty(x.Product.ExpiryDate.ToString()) && x.Product.ExpiryDate >= DateTime.Now)
                                                        }).ToList();
            List<UserDetails> userDetails = await _dbCollection.UserDBServices.GetUserDetails(userId);
            if (cartList == null || cartList.Count == 0)
                msg += "Your Cart is Empty. ";
            if (userDetails == null || userDetails.Count == 0)
                msg += "Please add delivery details to proceed. ";
            CartResponse cartResponse = new CartResponse(listProductQuantity, userDetails, msg);
            _logger.LogInformation(msg + "Product found Count:" + listProductQuantity?.Count);
            return cartResponse;
        }

        public async Task AddToCart(Cart cart)
        {
            var existingCart = await _dbCollection.CartDbService.CartItemExistsByProductId(cart.ProductId, cart.TokenUserId);
            if (existingCart != null)
            {
                _logger.LogInformation("Product already in cart. Increased product count by 1.");
                existingCart.Quantity = (Convert.ToInt32(cart.Quantity) + 1).ToString();
                await _dbCollection.CartDbService.Edit(existingCart);
            }
            else
            {
                _logger.LogInformation("Product added to cart successfully. productId:" + cart.ProductId);
                await _dbCollection.CartDbService.AddToCart(cart);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var cart = await _dbCollection.CartDbService.CartItemExists(id);
            if (cart != null)
            {
                _logger.LogInformation("Iteam Deleted successfully");
                await _dbCollection.CartDbService.Delete(cart);
                return true;
            }
            _logger.LogInformation("Iteam Not found");
            return false;
        }

        public async Task<bool> Edit(Cart cart)
        {
            var existingCart = await _dbCollection.CartDbService.CartItemExists(cart.CartId);
            if (existingCart != null)
            {
                if (cart.Quantity != "0")
                {
                    existingCart.Quantity = cart.Quantity;
                    await _dbCollection.CartDbService.Edit(existingCart);
                }
                else
                {
                    await _dbCollection.CartDbService.Delete(existingCart);
                }
                _logger.LogInformation("Cart updated successfully");
                return true;
            }
            _logger.LogInformation("Item not found");
            return false;
        }
    }
}
