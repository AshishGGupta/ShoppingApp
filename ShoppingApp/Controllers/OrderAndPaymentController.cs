namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Model;
    using ShoppingApp.ServiceInterfaces;
    using System.Threading.Tasks;

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
            var isSuccess = await _orderPaymentDetails.AddOrderPaymentDetails(orderPaymentRequest);
            if(isSuccess)
                return Ok("Order Placed Successfully");
            return NotFound("Cart Details not found");
        }

        [HttpGet("OrderAndPayment/GetOrderPaymentDetails")]
        public async Task<IActionResult> GetOrderPaymentDetails(string userId)
        {
            _logger.LogInformation("Entering GetOrderPaymentDetails method");
            var orderPaymentDetails = await _orderPaymentDetails.GetOrderPaymentDetails(userId);
            if(orderPaymentDetails?.Count > 0)
                return Ok(orderPaymentDetails);
            return NotFound("No order history found");
        }
    }
}
