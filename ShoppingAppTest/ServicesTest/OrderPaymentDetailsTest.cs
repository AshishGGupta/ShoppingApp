namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Services.Services;
    using ShoppingAppTest.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderPaymentDetailsTest
    {
        private readonly OrderPaymentDetails _orderPaymentServices;
        private readonly Mock<IOrderAndPaymentDBServices> _orderPaymentDBServices;
        private readonly Mock<ICartDbService> _cartDbServices;
        private readonly Mock<IDBServices> _productDbServices;
        private readonly Mock<ILogger<OrderPaymentDetails>> _logger;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public OrderPaymentDetailsTest()
        {
            _orderPaymentDBServices = new Mock<IOrderAndPaymentDBServices>();
            _cartDbServices = new Mock<ICartDbService>();
            _productDbServices = new Mock<IDBServices>();
            _logger = new Mock<ILogger<OrderPaymentDetails>>();
            _getData = new GetData();
            _orderPaymentServices = new OrderPaymentDetails(_orderPaymentDBServices.Object, _cartDbServices.Object, _productDbServices.Object, _logger.Object);
        }

        [Fact]
        public async Task AddOrderPaymentDetails_Success()
        {
            //Arrange
            var cartList = _getData.GetCartData();
            var orderPaymentDetails = _getData.GetOrderAndPaymentdetails().FirstOrDefault();
            var orderPaymentRequest = _getData.GetOrderAndPaymentRequest();
            _cartDbServices.Setup(x => x.GetCartDetails(userId)).ReturnsAsync(cartList);
            _cartDbServices.Setup(x => x.BulkCartDelete(cartList));
            _orderPaymentDBServices.Setup(x => x.AddOrderAndPaymentDetails(orderPaymentDetails));
            //Act
            var result = await _orderPaymentServices.AddOrderPaymentDetails(orderPaymentRequest);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task AddOrderPaymentDetails_CartDetailsNotFound()
        {
            //Arrange
            var orderPaymentRequest = _getData.GetOrderAndPaymentRequest();
            _cartDbServices.Setup(x => x.GetCartDetails(userId));
            //Act
            var result = await _orderPaymentServices.AddOrderPaymentDetails(orderPaymentRequest);
            //Assert
            Assert.False(result);
        }

        [Fact]
        public async Task GetOrderPaymentDetails_Success()
        {
            //Arrange
            var orderPaymentDetails = _getData.GetOrderAndPaymentdetails();
            var productList = _getData.GetProductsData();
            _orderPaymentDBServices.Setup(x => x.GetOrderAndPaymentDetails(userId)).ReturnsAsync(orderPaymentDetails);
            _productDbServices.Setup(x => x.GetProductByListOfId(It.IsAny<List<string>>())).ReturnsAsync(productList);
            //Act
            var result = await _orderPaymentServices.GetOrderPaymentDetails(userId);
            //Assert
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task GetOrderPaymentDetails_NoOrderDetailsFound()
        {
            //Arrange
            _orderPaymentDBServices.Setup(x => x.GetOrderAndPaymentDetails(userId));
            //Act
            var result = await _orderPaymentServices.GetOrderPaymentDetails(userId);
            //Assert
            Assert.Null(result);
        }
    }
}
