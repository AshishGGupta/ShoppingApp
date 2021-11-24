namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using ShoppingApp.Services.IServices;
    using ShoppingAppTest.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class UserDetailsControllerTest
    {
        private readonly UserDetailsController _userDetailsController;
        private readonly Mock<IUserDetailServices> _userDetailsServices;
        private readonly Mock<ILogger<UserDetailsController>> _logger;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public UserDetailsControllerTest()
        {
            _getData = new GetData();
            _userDetailsServices = new Mock<IUserDetailServices>();
            _logger = new Mock<ILogger<UserDetailsController>>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, userId),
                                        new Claim(ClaimTypes.Name, "user123@gmail.com")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            _userDetailsController = new UserDetailsController(_userDetailsServices.Object, _logger.Object);
            _userDetailsController.ControllerContext = new ControllerContext();
            _userDetailsController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };
        }

        [Fact]
        public async Task GetUserDetails_UnauthorizedUser()
        {
            //Act
            var result = await _userDetailsController.GetUserDetails("user890");
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetUserDetails_SuccessfullyFetchCartDetails()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.GetUserDetails(userId)).ReturnsAsync(_getData.GetUserDetailsData());
            //Act
            var result = await _userDetailsController.GetUserDetails(userId);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task GetUserDetails_NotDetialsFound()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.GetUserDetails(userId));
            //Act
            var result = await _userDetailsController.GetUserDetails(userId);
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task AddUserDetails_UnauthorizedUser()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            userDetail.TokenUserId = "user890";
            //Act
            var result = await _userDetailsController.AddUserDetails(userDetail);
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task AddUserDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _userDetailsServices.Setup(x => x.AddUserDetail(userDetail));
            //Act
            var result = await _userDetailsController.AddUserDetails(userDetail);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task UpdateUserDetails_UnauthorizedUser()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            userDetail.TokenUserId = "user890";
            //Act
            var result = await _userDetailsController.UpdateUserDetails(userDetail);
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task UpdateUserDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _userDetailsServices.Setup(x => x.EditUserDetail(userDetail)).ReturnsAsync(true);
            //Act
            var result = await _userDetailsController.UpdateUserDetails(userDetail);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task UpdateUserDetails_NotDetialsFound()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _userDetailsServices.Setup(x => x.EditUserDetail(userDetail)).ReturnsAsync(false);
            //Act
            var result = await _userDetailsController.UpdateUserDetails(userDetail);
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task DeleteUserDetails_UnauthorizedUser()
        {
            //Act
            var result = await _userDetailsController.DeleteUserDetails(1, "user890");
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task DeleteUserDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.DeleteUserDetail(1, userId)).ReturnsAsync(true);
            //Act
            var result = await _userDetailsController.DeleteUserDetails(1, userId);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task DeleteUserDetails_NotDetialsFound()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.DeleteUserDetail(1, userId)).ReturnsAsync(false);
            //Act
            var result = await _userDetailsController.DeleteUserDetails(1, userId);
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }
    }
}
