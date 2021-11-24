namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using ShoppingApp.Services.IServices;
    using ShoppingAppTest.Common;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderAndPaymentControllerTest
    {
        private readonly OrderAndPaymentController _orderPaymentController;
        private readonly Mock<IOrderPaymentDetails> _orderPaymentServices;
        private readonly Mock<ILogger<OrderAndPaymentController>> _logger;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public OrderAndPaymentControllerTest()
        {
            _getData = new GetData();
            _orderPaymentServices = new Mock<IOrderPaymentDetails>();
            _logger = new Mock<ILogger<OrderAndPaymentController>>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, userId),
                                        new Claim(ClaimTypes.Name, "user123@gmail.com")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            _orderPaymentController = new OrderAndPaymentController(_orderPaymentServices.Object, _logger.Object);
            _orderPaymentController.ControllerContext = new ControllerContext();
            _orderPaymentController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }

        [Fact]
        public async Task AddOrderPaymentDetails_UnauthorizedUser()
        {
            //arrange
            var input = _getData.GetOrderAndPaymentRequest();
            input.TokenUserId = "user890";
            //Act
            var result = await _orderPaymentController.AddOrderPaymentDetails(input);
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task AddOrderPaymentDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            var orderPaymentRequest = _getData.GetOrderAndPaymentRequest();
            _orderPaymentServices.Setup(x => x.AddOrderPaymentDetails(orderPaymentRequest)).ReturnsAsync(true);
            //Act
            var result = await _orderPaymentController.AddOrderPaymentDetails(orderPaymentRequest);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task AddOrderPaymentDetails_NotDetialsFound()
        {
            //Arrange
            var orderPaymentRequest = _getData.GetOrderAndPaymentRequest();
            _orderPaymentServices.Setup(x => x.AddOrderPaymentDetails(orderPaymentRequest)).ReturnsAsync(false);
            //Act
            var result = await _orderPaymentController.AddOrderPaymentDetails(orderPaymentRequest);
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetOrderPaymentDetails_UnauthorizedUser()
        {
            //Act
            var result = await _orderPaymentController.GetOrderPaymentDetails("user890");
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetOrderPaymentDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            _orderPaymentServices.Setup(x => x.GetOrderPaymentDetails(userId)).ReturnsAsync(_getData.GetOrderAndPaymentResponse());
            //Act
            var result = await _orderPaymentController.GetOrderPaymentDetails(userId);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetOrderPaymentDetails_NotDetialsFound()
        {
            //Arrange
            _orderPaymentServices.Setup(x => x.GetOrderPaymentDetails(userId));
            //Act
            var result = await _orderPaymentController.GetOrderPaymentDetails(userId);
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }
    }
}
