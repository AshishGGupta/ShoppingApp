namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Models.Model;
    using Xunit;
    using System.Threading.Tasks;
    using MediatR;
    using ShoppingApp.Models.MediatorClass;
    using System.Threading;

    public class UserControllerTest
    {
        private readonly UserController _userController;
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<ILogger<UserController>> _logger;

        public UserControllerTest()
        {
            _mediator = new Mock<IMediator>();
            _logger = new Mock<ILogger<UserController>>();

            _userController = new UserController(_mediator.Object, _logger.Object);
        }

        [Fact]
        public async Task SignUp_Success()
        {
            //Arrange
            Signup userModel = new Signup()
            {
                UserName = "user123",
                Password = "pass123321"
            };
            _mediator.Setup(x => x.Send(It.IsAny<Signup>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ApiResponse());
            //Act
            var result = await _userController.SignUp(userModel);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public async Task Login_Success()
        {
            //Arrange
            LoginUser userModel = new LoginUser()
            {
                UserName = "user123",
                Password = "pass123321"
            };
            _mediator.Setup(x => x.Send(It.IsAny<LoginUser>(), It.IsAny<CancellationToken>())).ReturnsAsync(new ApiResponse());
            //Act
            var result = await _userController.Login(userModel);
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }
    }
}
