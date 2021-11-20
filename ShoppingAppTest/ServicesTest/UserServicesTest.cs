namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Moq;
    using Moq.Protected;
    using Newtonsoft.Json;
    using ShoppingApp.IDataAccess;
    using ShoppingApp.Model;
    using ShoppingApp.Services;
    using ShoppingAppTest.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class UserServicesTest
    {
        private readonly Mock<IOptions<SignUpDetails>> signupDetails;
        private readonly Mock<IOptions<LoginDetails>> loginDetails;
        private readonly Mock<IDBServices> _dbServices;
        private readonly Mock<ILogger<UserServices>> _logger;
        private readonly Mock<HttpClient> _httpClient;
        private readonly UserServices _userServices;
        private readonly GetData _getData;

        public UserServicesTest()
        {
            _getData = new GetData();
            signupDetails = new Mock<IOptions<SignUpDetails>>();
            loginDetails = new Mock<IOptions<LoginDetails>>();
            _dbServices = new Mock<IDBServices>();
            _httpClient = new Mock<HttpClient>();
            _logger = new Mock<ILogger<UserServices>>();
            _userServices = new UserServices(signupDetails.Object, loginDetails.Object, _dbServices.Object, _logger.Object);
        }

        //[Fact]
        //public async Task SignUp_exception()
        //{
        //    //Arrange
        //    var userModel = _getData.GetUserModelData();
        //    _dbServices.Setup(x => x.UserExists(userModel.UserName)).ReturnsAsync(true);
        //    _dbServices.Setup(x => x.RegisterUser(_userServices.MapUser(userModel.UserName, userModel.Password)));

        //    HttpResponseMessage message = new HttpResponseMessage();
        //    var stringContent = new StringContent(JsonConvert.SerializeObject(_getData.GetAuthResponseData()), UnicodeEncoding.UTF8, "application/json");
        //    message.Content = stringContent;
            
        //    //_httpClient.Setup(x => x.PostAsync("", stringContent)).ReturnsAsync(message);

        //    var mockMessageHandler = new Mock<HttpMessageHandler>();
        //    mockMessageHandler.Protected()
        //        .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>())
        //        .ReturnsAsync(new HttpResponseMessage
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = stringContent
        //        });
        //    //Act
        //    var result = await _userServices.UserSignUp(userModel);
        //}
    }
}
