namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using ShoppingApp.Interfaces;
    using ShoppingApp.Model;
    using Xunit;

    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IUserServices> _userServices;
        private readonly Mock<ILogger<UserController>> _logger;

        public UserControllerTest()
        {
            _userServices = new Mock<IUserServices>();
            _logger = new Mock<ILogger<UserController>>();

            _userController = new UserController(_userServices.Object, _logger.Object);
        }

        [Fact]
        public void SignUp_Success()
        {
            //Arrange
            UserModel userModel = new UserModel()
            {
                UserName = "user123",
                Password = "pass123321"
            };
            _userServices.Setup(x => x.UserSignUp(userModel)).ReturnsAsync(new ApiResponse());
            //Act
            var result = _userController.SignUp(userModel).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void Login_Success()
        {
            //Arrange
            UserModel userModel = new UserModel()
            {
                UserName = "user123",
                Password = "pass123321"
            };
            _userServices.Setup(x => x.UserLogin(userModel)).ReturnsAsync(new ApiResponse());
            //Act
            var result = _userController.Login(userModel).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }
    }
}
