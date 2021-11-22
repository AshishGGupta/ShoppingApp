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
        public void GetUserDetails_UnauthorizedUser()
        {
            //Act
            var result = _userDetailsController.GetUserDetails("user890").Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetUserDetails_SuccessfullyFetchCartDetails()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.GetUserDetails(userId)).ReturnsAsync(_getData.GetUserDetailsData());
            //Act
            var result = _userDetailsController.GetUserDetails(userId).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetUserDetails_NotDetialsFound()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.GetUserDetails(userId));
            //Act
            var result = _userDetailsController.GetUserDetails(userId).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void AddUserDetails_UnauthorizedUser()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            userDetail.TokenUserId = "user890";
            //Act
            var result = _userDetailsController.AddUserDetails(userDetail).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void AddUserDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _userDetailsServices.Setup(x => x.AddUserDetail(userDetail));
            //Act
            var result = _userDetailsController.AddUserDetails(userDetail).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateUserDetails_UnauthorizedUser()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            userDetail.TokenUserId = "user890";
            //Act
            var result = _userDetailsController.UpdateUserDetails(userDetail).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateUserDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _userDetailsServices.Setup(x => x.EditUserDetail(userDetail)).ReturnsAsync(true);
            //Act
            var result = _userDetailsController.UpdateUserDetails(userDetail).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateUserDetails_NotDetialsFound()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _userDetailsServices.Setup(x => x.EditUserDetail(userDetail)).ReturnsAsync(false);
            //Act
            var result = _userDetailsController.UpdateUserDetails(userDetail).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteUserDetails_UnauthorizedUser()
        {
            //Act
            var result = _userDetailsController.DeleteUserDetails(1, "user890").Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteUserDetails_SuccessfullyAddedToCart()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.DeleteUserDetail(1, userId)).ReturnsAsync(true);
            //Act
            var result = _userDetailsController.DeleteUserDetails(1, userId).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteUserDetails_NotDetialsFound()
        {
            //Arrange
            _userDetailsServices.Setup(x => x.DeleteUserDetail(1, userId)).ReturnsAsync(false);
            //Act
            var result = _userDetailsController.DeleteUserDetails(1, userId).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }
    }
}
