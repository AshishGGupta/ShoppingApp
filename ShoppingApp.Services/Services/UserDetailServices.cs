namespace ShoppingApp.Services.Services
{
    using Microsoft.Extensions.Logging;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserDetailServices : IUserDetailServices
    {
        private readonly IUserDbServices _userDbServices;
        private readonly ILogger<UserDetailServices> _logger;

        public UserDetailServices(IUserDbServices userDbServices, ILogger<UserDetailServices> logger)
        {
            _userDbServices = userDbServices;
            _logger = logger;
        }

        public async Task<List<UserDetails>> GetUserDetails(string userId)
        {
            return await _userDbServices.GetUserDetails(userId);
        }

        public async Task AddUserDetail(UserDetails userDetails)
        {
            await _userDbServices.AddUserDetails(userDetails);
        }

        public async Task<bool> EditUserDetail(UserDetails userDetails)
        {
            var existingUserDetails = await _userDbServices.UserItemExists(userDetails.Id, userDetails.TokenUserId);
            if (existingUserDetails != null)
            {
                existingUserDetails.Address = userDetails.Address;
                existingUserDetails.PhoneNumber = userDetails.PhoneNumber;
                await _userDbServices.UpdateUserDetails(existingUserDetails);
                _logger.LogInformation("User details updated successfully");
                return true;
            }
            _logger.LogInformation("User details not found");
            return false;
        }

        public async Task<bool> DeleteUserDetail(int userDetailsId, string userId)
        {
            var userDetails = await _userDbServices.UserItemExists(userDetailsId, userId);
            if (userDetails != null)
            {
                await _userDbServices.DeleteUserDetail(userDetails);
                _logger.LogInformation("User details deleted successfully");
                return true;
            }
            _logger.LogInformation("User details not found");
            return false;
        }
    }
}
