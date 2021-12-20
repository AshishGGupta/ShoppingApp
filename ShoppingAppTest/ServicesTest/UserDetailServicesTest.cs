using Microsoft.Extensions.Logging;
using Moq;
using ShoppingApp.DataAccess.IDataAccess;
using ShoppingApp.Services.Services;
using ShoppingAppTest.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAppTest.ServicesTest
{
    public class UserDetailServicesTest
    {
        private readonly Mock<IDbFacade> _dbFacade;
        private readonly Mock<ILogger<UserDetailServices>> _logger;
        private readonly UserDetailServices _userDetailServices;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public UserDetailServicesTest()
        {
            _getData = new GetData();
            _dbFacade = new Mock<IDbFacade>();
            _logger = new Mock<ILogger<UserDetailServices>>();
            _userDetailServices = new UserDetailServices(_dbFacade.Object, _logger.Object);
        }

        [Fact]
        public async Task GetUserDetails_Success()
        {
            //Arrange
            _dbFacade.Setup(x => x.UserDBServices.GetUserDetails(userId)).ReturnsAsync(_getData.GetUserDetailsData());
            //Act
            var result = await _userDetailServices.GetUserDetails(userId);
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task AddUserDetail_Success()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _dbFacade.Setup(x => x.UserDBServices.AddItem(userDetail));
            //Act
            await _userDetailServices.AddUserDetail(userDetail);
            //Assert
            Assert.True(true);
        }

        [Fact]
        public async Task EditUserDetail_Success()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _dbFacade.Setup(x => x.UserDBServices.GetItemById(x => x.Id == userDetail.Id && x.TokenUserId == userDetail.TokenUserId)).ReturnsAsync(userDetail);
            _dbFacade.Setup(x => x.UserDBServices.UpdateItem(userDetail));
            //Act
            bool isSuccess = await _userDetailServices.EditUserDetail(userDetail);
            //Assert
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task EditUserDetail_NotFound()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _dbFacade.Setup(x => x.UserDBServices.GetItemById(x => x.Id == userDetail.Id && x.TokenUserId == userDetail.TokenUserId));
            _dbFacade.Setup(x => x.UserDBServices.UpdateItem(userDetail));
            //Act
            bool isSuccess = await _userDetailServices.EditUserDetail(userDetail);
            //Assert
            Assert.False(isSuccess);
        }

        [Fact]
        public async Task DeleteUserDetail_Success()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            var userDetailsId = userDetail.Id;
            var userId = userDetail.TokenUserId;
            _dbFacade.Setup(x => x.UserDBServices.GetItemById(x => x.Id == userDetailsId && x.TokenUserId == userId)).ReturnsAsync(userDetail);
            _dbFacade.Setup(x => x.UserDBServices.DeleteItem(userDetail));
            //Act
            bool isSuccess = await _userDetailServices.DeleteUserDetail(userDetail.Id, userDetail.TokenUserId);
            //Assert
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task DeleteUserDetail_NotFound()
        {
            //Arrange
            var userDetail = _getData.GetUserDetailsData().FirstOrDefault();
            _dbFacade.Setup(x => x.UserDBServices.GetItemById(x => x.Id == userDetail.Id && x.TokenUserId == userDetail.TokenUserId));
            _dbFacade.Setup(x => x.UserDBServices.DeleteItem(userDetail));
            //Act
            bool isSuccess = await _userDetailServices.DeleteUserDetail(userDetail.Id, userDetail.TokenUserId);
            //Assert
            Assert.False(isSuccess);
        }
    }
}
