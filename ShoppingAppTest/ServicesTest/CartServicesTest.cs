namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Services.Services;
    using ShoppingAppTest.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class CartServicesTest
    {
        private readonly Mock<IDbFacade> _dbFacade;
        private readonly Mock<ILogger<CartServices>> _logger;
        private readonly CartServices _cartServices;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public CartServicesTest()
        {
            _getData = new GetData();
            _dbFacade = new Mock<IDbFacade>();
            _logger = new Mock<ILogger<CartServices>>();
            _cartServices = new CartServices(_dbFacade.Object, _logger.Object);
        }

        [Fact]
        public void GetCartDetails_Success()
        {
            //Arrange
            _dbFacade.Setup(x => x.CartDbService.GetCartDetails(userId)).ReturnsAsync(_getData.GetCartData());
            _dbFacade.Setup(x => x.UserDBServices.GetUserDetails(userId)).ReturnsAsync(_getData.GetUserDetailsData());
            //Act
            var result = _cartServices.GetCartDetails(userId).Result;
            //Assert
            Assert.NotEmpty((result.ProductQuantity));
        }

        [Fact]
        public void GetCartDetails_EmptyCartList()
        {
            //Arrange
            _dbFacade.Setup(x => x.CartDbService.GetCartDetails(userId));
            _dbFacade.Setup(x => x.UserDBServices.GetUserDetails(userId));
            //Act
            var result = _cartServices.GetCartDetails(userId).Result;
            //Assert
            Assert.Null((result.ProductQuantity));
        }

        [Fact]
        public async Task AddToCart_Success()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _dbFacade.Setup(x => x.CartDbService.CartItemExistsByProductId(cart.CartId, userId));
            _dbFacade.Setup(x => x.CartDbService.AddItem(cart));
            //Act
            await _cartServices.AddToCart(cart);
            //Assert
            Assert.True(true);
        }

        [Fact]
        public async Task AddToCart_AlreadyDataAdded()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _dbFacade.Setup(x => x.CartDbService.CartItemExistsByProductId(cart.CartId, userId)).ReturnsAsync(cart);
            _dbFacade.Setup(x => x.CartDbService.UpdateItem(cart));
            //Act
            await _cartServices.AddToCart(cart);
            //Assert
            Assert.True(true);
        }

        [Fact]
        public void DeleteCart_Success()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            var id = cart.CartId;
            _dbFacade.Setup(x => x.CartDbService.GetItemById(x => x.CartId == id)).ReturnsAsync(cart);
            _dbFacade.Setup(x => x.CartDbService.DeleteItem(cart));
            //Act
            bool isSuccess = _cartServices.Delete(cart.CartId).Result;
            //Assert
            Assert.True(isSuccess);
        }

        [Fact]
        public void DeleteCart_CartNotFound()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _dbFacade.Setup(x => x.CartDbService.GetItemById(x => x.CartId == cart.CartId));
            _dbFacade.Setup(x => x.CartDbService.DeleteItem(cart));
            //Act
            bool isSuccess = _cartServices.Delete(cart.CartId).Result;
            //Assert
            Assert.False(isSuccess);
        }

        [Fact]
        public void UpdateCart_Success()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _dbFacade.Setup(x => x.CartDbService.GetItemById(x => x.CartId == cart.CartId)).ReturnsAsync(cart);
            _dbFacade.Setup(x => x.CartDbService.UpdateItem(cart));
            //Act
            bool isSuccess = _cartServices.Edit(cart).Result;
            //Assert
            Assert.True(isSuccess);
        }

        [Fact]
        public void UpdateCart_NotFound()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _dbFacade.Setup(x => x.CartDbService.GetItemById(x => x.CartId == cart.CartId));
            _dbFacade.Setup(x => x.CartDbService.UpdateItem(cart));
            //Act
            bool isSuccess = _cartServices.Edit(cart).Result;
            //Assert
            Assert.False(isSuccess);
        }

        [Fact]
        public void UpdateCart_RemoveCart()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            cart.Quantity = "0";
            _dbFacade.Setup(x => x.CartDbService.GetItemById(x => x.CartId == cart.CartId)).ReturnsAsync(cart);
            _dbFacade.Setup(x => x.CartDbService.UpdateItem(cart));
            //Act
            bool isSuccess = _cartServices.Edit(cart).Result;
            //Assert
            Assert.True(isSuccess);
        }
    }
}
