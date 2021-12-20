namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Services.Services;
    using ShoppingAppTest.Common;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class OrderPaymentDetailsTest
    {
        private readonly OrderPaymentDetails _orderPaymentServices;
        private readonly Mock<IDbFacade> _dbFacade;
        private readonly Mock<ILogger<OrderPaymentDetails>> _logger;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public OrderPaymentDetailsTest()
        {
            _dbFacade = new Mock<IDbFacade>();
            _logger = new Mock<ILogger<OrderPaymentDetails>>();
            _getData = new GetData();
            _orderPaymentServices = new OrderPaymentDetails(_dbFacade.Object, _logger.Object);
        }

        [Fact]
        public async Task AddOrderPaymentDetails_Success()
        {
            //Arrange
            var cartList = _getData.GetCartData();
            var orderPaymentDetails = _getData.GetOrderAndPaymentdetails();
            var orderPaymentRequest = _getData.GetOrderAndPaymentRequest();
            _dbFacade.Setup(x => x.CartDbService.GetCartDetails(userId)).ReturnsAsync(cartList);
            _dbFacade.Setup(x => x.CartDbService.BulkCartDelete(cartList));
            _dbFacade.Setup(x => x.OrderDBServices.AddOrderAndPaymentDetails(orderPaymentDetails));
            _dbFacade.Setup(x => x.ProductDbServices.UpdateItem(It.IsAny<Product>()));
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
            _dbFacade.Setup(x => x.CartDbService.GetCartDetails(userId));
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
            _dbFacade.Setup(x => x.OrderDBServices.GetOrderAndPaymentDetails(userId)).ReturnsAsync(orderPaymentDetails);
            _dbFacade.Setup(x => x.ProductDbServices.GetProductByListOfId(It.IsAny<List<string>>())).ReturnsAsync(productList);
            //Act
            var result = await _orderPaymentServices.GetOrderPaymentDetails(userId);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetOrderPaymentDetails_NoOrderDetailsFound()
        {
            //Arrange
            _dbFacade.Setup(x => x.OrderDBServices.GetOrderAndPaymentDetails(userId));
            //Act
            var result = await _orderPaymentServices.GetOrderPaymentDetails(userId);
            //Assert
            Assert.Null(result);
        }
    }
}
