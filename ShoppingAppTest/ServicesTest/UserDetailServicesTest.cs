using Microsoft.Extensions.Logging;
using Moq;
using ShoppingApp.IDataAccess;
using ShoppingApp.Services;
using ShoppingAppTest.Common;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace ShoppingAppTest.ServicesTest
{
    public class UserDetailServicesTest
    {
        private readonly Mock<IUserDbServices> _userDetailDbService;
        private readonly Mock<ILogger<UserDetailServices>> _logger;
        private readonly UserDetailServices _userDetailServices;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public UserDetailServicesTest()
        {
            _getData = new GetData();
            _userDetailDbService = new Mock<IUserDbServices>();
            _logger = new Mock<ILogger<UserDetailServices>>();
            _userDetailServices = new UserDetailServices(_userDetailDbService.Object, _logger.Object);
        }

        [Fact]
        public async Task GetUserDetails_Success()
        {
            //Arrange
            _userDetailDbService.Setup(x => x.GetUserDetails(userId)).ReturnsAsync(_getData.GetUserDetailsData());
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
            _userDetailDbService.Setup(x => x.AddUserDetails(userDetail));
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
            _userDetailDbService.Setup(x => x.UserItemExists(userDetail.Id, userDetail.TokenUserId)).ReturnsAsync(userDetail);
            _userDetailDbService.Setup(x => x.UpdateUserDetails(userDetail));
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
            _userDetailDbService.Setup(x => x.UserItemExists(userDetail.Id, userDetail.TokenUserId));
            _userDetailDbService.Setup(x => x.UpdateUserDetails(userDetail));
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
            _userDetailDbService.Setup(x => x.UserItemExists(userDetail.Id, userDetail.TokenUserId)).ReturnsAsync(userDetail);
            _userDetailDbService.Setup(x => x.DeleteUserDetail(userDetail));
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
            _userDetailDbService.Setup(x => x.UserItemExists(userDetail.Id, userDetail.TokenUserId));
            _userDetailDbService.Setup(x => x.DeleteUserDetail(userDetail));
            //Act
            bool isSuccess = await _userDetailServices.DeleteUserDetail(userDetail.Id, userDetail.TokenUserId);
            //Assert
            Assert.False(isSuccess);
        }
    }
}
