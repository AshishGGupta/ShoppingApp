namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Services.Validation;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Authorize]
    [ServiceFilter(typeof(ExceptionAttribute))]
    public class OrderAndPaymentController : Controller
    {
        private readonly IOrderPaymentDetails _orderPaymentDetails;
        private readonly ILogger<OrderAndPaymentController> _logger;

        public OrderAndPaymentController(IOrderPaymentDetails orderPaymentDetails, ILogger<OrderAndPaymentController> logger)
        {
            _orderPaymentDetails = orderPaymentDetails;
            _logger = logger;
        }

        [HttpPost("OrderAndPayment/AddOrderPaymentDetails")]
        public async Task<IActionResult> AddOrderPaymentDetails([FromBody] OrderAndPaymentRequest orderPaymentRequest)
        {
            _logger.LogInformation("Entering AddOrderPaymentDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != orderPaymentRequest.TokenUserId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            var isSuccess = await _orderPaymentDetails.AddOrderPaymentDetails(orderPaymentRequest);
            if(isSuccess)
                return Ok("Order Placed Successfully");
            return NotFound("Cart Details not found");
        }

        [HttpGet("OrderAndPayment/GetOrderPaymentDetails/{userId}")]
        public async Task<IActionResult> GetOrderPaymentDetails(string userId)
        {
            _logger.LogInformation("Entering GetOrderPaymentDetails method");
            string authUserId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (authUserId != userId)
            {
                _logger.LogInformation("User is not authorized to perform this operation.");
                return Unauthorized("Your are not authorized");
            }
            var orderPaymentDetails = await _orderPaymentDetails.GetOrderPaymentDetails(userId);
            if(orderPaymentDetails?.Count > 0)
                return Ok(orderPaymentDetails);
            return NotFound("No order history found");
        }
    }
}
