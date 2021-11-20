﻿namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using ShoppingApp.Interfaces;
    using ShoppingAppTest.Common;
    using System.Linq;
    using System.Security.Claims;
    using Xunit;
    public class CartControllerTest
    {
        private readonly CartController _cartController;
        private readonly Mock<ICartServices> _cartServices;
        private readonly Mock<ILogger<CartController>> _logger;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public CartControllerTest()
        {
            _getData = new GetData();
            _cartServices = new Mock<ICartServices>();
            _logger = new Mock<ILogger<CartController>>();
            
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, userId),
                                        new Claim(ClaimTypes.Name, "user123@gmail.com")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            _cartController = new CartController(_cartServices.Object, _logger.Object);
            _cartController.ControllerContext = new ControllerContext();
            _cartController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }

        [Fact]
        public void GetCartDetails_UserNotLoggedin_EmptyCart()
        {
            //Act
            var result = _cartController.GetCartDetails();
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void GetCartDetails_UnauthorizedUser()
        {
            //Act
            var result = _cartController.GetCartDetails("user890").Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetCartDetails_SuccessfullyFetchCartDetails()
        {
            //Arrange
            _cartServices.Setup(x => x.GetCartDetails(userId)).ReturnsAsync(_getData.GetCartResponseData());
            //Act
            var result = _cartController.GetCartDetails(userId).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetCartDetails_NotDetialsFound()
        {
            //Arrange
            _cartServices.Setup(x => x.GetCartDetails(userId));
            //Act
            var result = _cartController.GetCartDetails(userId).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void AddToCart_UnauthorizedUser()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            cart.TokenUserId = "user890";
            //Act
            var result = _cartController.AddToCart(cart).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void AddToCart_SuccessfullyAddedToCart()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _cartServices.Setup(x => x.AddToCart(cart));
            //Act
            var result = _cartController.AddToCart(cart).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateCartDetails_UnauthorizedUser()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            cart.TokenUserId = "user890";
            //Act
            var result = _cartController.UpdateCartDetails(cart).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateCartDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _cartServices.Setup(x => x.Edit(cart)).ReturnsAsync(true);
            //Act
            var result = _cartController.UpdateCartDetails(cart).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateCartDetails_NotDetialsFound()
        {
            //Arrange
            var cart = _getData.GetCartData().FirstOrDefault();
            _cartServices.Setup(x => x.Edit(cart)).ReturnsAsync(false);
            //Act
            var result = _cartController.UpdateCartDetails(cart).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteFromCart_UnauthorizedUser()
        {
            //Act
            var result = _cartController.DeleteFromCart(1, "user890").Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteFromCart_SuccessfullyAddedToCart()
        {
            //Arrange
            _cartServices.Setup(x => x.Delete(1)).ReturnsAsync(true);
            //Act
            var result = _cartController.DeleteFromCart(1, userId).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteFromCart_NotDetialsFound()
        {
            //Arrange
            _cartServices.Setup(x => x.Delete(1)).ReturnsAsync(false);
            //Act
            var result = _cartController.DeleteFromCart(1, userId).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }
    }
}