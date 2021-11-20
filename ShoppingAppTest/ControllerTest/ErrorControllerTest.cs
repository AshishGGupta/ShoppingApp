namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using System;
    using Xunit;

    public class ErrorControllerTest
    {
        private readonly Mock<ILogger<ErrorController>> _logger;
        private readonly ErrorController _errorController;
        public ErrorControllerTest()
        {
            _logger = new Mock<ILogger<ErrorController>>();
            _errorController = new ErrorController(_logger.Object);
        }

        [Fact]
        public void GetCartDetails_UserNotLoggedin_EmptyCart()
        {
            //Act
            var result = _errorController.Error(new NullReferenceException());
            //Assert
            Assert.Equal(500, (result as StatusCodeResult).StatusCode);
        }
    }
}
