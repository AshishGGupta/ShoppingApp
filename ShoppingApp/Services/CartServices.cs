using ShoppingApp.IDataAccess;
using ShoppingApp.Interfaces;
using ShoppingApp.Model;
using ShoppingApp.Model.Domain;
using System;
using System.Collections.Generic;
namespace ShoppingApp.Services
{
    using Microsoft.Extensions.Logging;
    using System.Linq;
    using System.Threading.Tasks;

    public class CartServices : ICartServices
    {
        private readonly ICartDbService _cartDbService;
        private readonly IUserDbServices _userDbServices;
        private readonly ILogger<CartServices> _logger;

        public CartServices(ICartDbService cartDbService, IUserDbServices userDbServices, ILogger<CartServices> logger)
        {
            _cartDbService = cartDbService;
            _userDbServices = userDbServices;
            _logger = logger;
        }

        public async Task<CartResponse> GetCartDetails(string userId)
        {
            string msg = "";
            List<Cart> cartList = await _cartDbService.GetCartDetails(userId);
            List<ProductQuantity> listProductQuantity = cartList?.Select(x => new ProductQuantity()
                                                        {
                                                            CartId = x.CartId,
                                                            Product = x.Product,
                                                            Quantity = Convert.ToInt32(x.Quantity),
                                                            ProductExpired = (!string.IsNullOrEmpty(x.Product.ExpiryDate.ToString()) && x.Product.ExpiryDate >= DateTime.Now)
                                                        }).ToList();
            List<UserDetails> userDetails = await _userDbServices.GetUserDetails(userId);
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
            var existingCart = await _cartDbService.CartItemExistsByProductId(cart.ProductId);
            if (existingCart != null)
            {
                _logger.LogInformation("Product already in cart. Increased product count by 1.");
                existingCart.Quantity = (Convert.ToInt32(cart.Quantity) + 1).ToString();
                await _cartDbService.Edit(existingCart);
            }
            else
            {
                _logger.LogInformation("Product added to cart successfully. productId:" + cart.ProductId);
                await _cartDbService.AddToCart(cart);
            }
        }

        public async Task<bool> Delete(int id)
        {
            var cart = await _cartDbService.CartItemExists(id);
            if (cart != null)
            {
                _logger.LogInformation("Iteam Deleted successfully");
                await _cartDbService.Delete(cart);
                return true;
            }
            _logger.LogInformation("Iteam Not found");
            return false;
        }

        public async Task<bool> Edit(Cart cart)
        {
            var existingCart = await _cartDbService.CartItemExists(cart.CartId);
            if (existingCart != null)
            {
                if (cart.Quantity != "0")
                {
                    existingCart.Quantity = cart.Quantity;
                    await _cartDbService.Edit(existingCart);
                }
                else
                {
                    await _cartDbService.Delete(existingCart);
                }
                _logger.LogInformation("Cart updated successfully");
                return true;
            }
            _logger.LogInformation("Item not found");
            return false;
        }
    }
}
