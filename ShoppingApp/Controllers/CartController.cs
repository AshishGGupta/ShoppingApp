namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Services.Validation;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    [ServiceFilter(typeof(ExceptionAttribute))]
    public class CartController : Controller
    {
        private readonly ICartServices _cartServices;
        private readonly ILogger<CartController> _logger;

        public CartController(ICartServices cartServices, ILogger<CartController> logger)
        {
            _cartServices = cartServices;
            _logger = logger;
        }

        [HttpGet("Cart/GetCartDetails")]
        public IActionResult GetCartDetails()
        {
            _logger.LogInformation("Entering GetCartDetails method");
            return Ok("Please Login to check your cart details!");
        }

        [HttpGet("Cart/GetCartDetails/{userId}")]
        public async Task<IActionResult> GetCartDetails(string userId)
        {
            _logger.LogInformation("Entering GetCartDetails userId method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            CartResponse cartlist = await _cartServices.GetCartDetails(userId);
            if (cartlist != null)
            {
                return Ok(cartlist);
            }
            return NotFound("Your Cart is Empty");
        }

        [HttpPost("Cart/AddToCart")]
        public async Task<IActionResult> AddToCart([FromBody] Cart cart)
        {
            _logger.LogInformation("Entering AddToCart method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != cart.TokenUserId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            await _cartServices.AddToCart(cart);
            return Ok("Product added to Cart Successfully");
        }

        [HttpPut("Cart/UpdateCartDetails")]
        public async Task<IActionResult> UpdateCartDetails([FromBody] Cart cart)
        {
            _logger.LogInformation("Entering UpdateCartDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != cart.TokenUserId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            bool isSuccess = await _cartServices.Edit(cart);
            if (isSuccess)
            {
                return Ok("Cart details updated successfully");
            }
            return NotFound("Product Not Found");
        }

        [HttpDelete("Cart/DeleteFromCart/{id}/{userId}")]
        public async Task<IActionResult> DeleteFromCart(int id, string userId)
        {
            _logger.LogInformation("Entering DeleteCart method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            bool isDeleted = await _cartServices.Delete(id);
            if (isDeleted)
                return Ok("cart item Deleted Successfully");
            return NotFound("Cart item not found");
        }
    }
}
