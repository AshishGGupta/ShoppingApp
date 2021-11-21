namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.IDataAccess;
    using ShoppingApp.Services;
    using ShoppingAppTest.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class CartServicesTest
    {
        private readonly Mock<ICartDbService> _cartDbService;
        private readonly Mock<IUserDbServices> _userDbServices;
        private readonly Mock<ILogger<CartServices>> _logger;
        private readonly CartServices _cartServices;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public CartServicesTest()
        {
            _getData = new GetData();
            _cartDbService = new Mock<ICartDbService>();
            _userDbServices = new Mock<IUserDbServices>();
            _logger = new Mock<ILogger<CartServices>>();
            _cartServices = new CartServices(_cartDbService.Object, _userDbServices.Object, _logger.Object);
        }

        [Fact]
        public void GetCartDetails_Success()
        {
            //Arrange
            _cartDbService.Setup(x => x.GetCartDetails(userId)).ReturnsAsync(_getData.GetCartData());
            _userDbServices.Setup(x => x.GetUserDetails(userId)).ReturnsAsync(_getData.GetUserDetailsData());
            //Act
            var result = _cartServices.GetCartDetails(userId).Result;
            //Assert
            Assert.NotEmpty((result.ProductQuantity));
        }

        [Fact]
        public void GetCartDetails_EmptyCartList()
        {
            //Arrange
            _cartDbService.Setup(x => x.GetCartDetails(userId));
            _userDbServices.Setup(x => x.GetUserDetails(userId));
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
            _cartDbService.Setup(x => x.CartItemExistsByProductId(cart.CartId, userId));
            _cartDbService.Setup(x => x.AddToCart(cart));
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
            _cartDbService.Setup(x => x.CartItemExistsByProductId(cart.CartId, userId)).ReturnsAsync(cart);
            _cartDbService.Setup(x => x.Edit(cart));
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
            _cartDbService.Setup(x => x.CartItemExists(cart.CartId)).ReturnsAsync(cart);
            _cartDbService.Setup(x => x.Delete(cart));
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
            _cartDbService.Setup(x => x.CartItemExists(cart.CartId));
            _cartDbService.Setup(x => x.Delete(cart));
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
            _cartDbService.Setup(x => x.CartItemExists(cart.CartId)).ReturnsAsync(cart);
            _cartDbService.Setup(x => x.Edit(cart));
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
            _cartDbService.Setup(x => x.CartItemExists(cart.CartId));
            _cartDbService.Setup(x => x.Edit(cart));
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
            _cartDbService.Setup(x => x.CartItemExists(cart.CartId)).ReturnsAsync(cart);
            _cartDbService.Setup(x => x.Edit(cart));
            //Act
            bool isSuccess = _cartServices.Edit(cart).Result;
            //Assert
            Assert.True(isSuccess);
        }
    }
}
