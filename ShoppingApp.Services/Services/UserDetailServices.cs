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
        private readonly IDbFacade _dbCollection;
        private readonly ILogger<UserDetailServices> _logger;

        public UserDetailServices(IDbFacade dbFacade, ILogger<UserDetailServices> logger)
        {
            _dbCollection = dbFacade;
            _logger = logger;
        }

        public async Task<List<UserDetails>> GetUserDetails(string userId)
        {
            return await _dbCollection.UserDBServices.GetUserDetails(userId);
        }

        public async Task AddUserDetail(UserDetails userDetails)
        {
            await _dbCollection.UserDBServices.AddItem(userDetails);
        }

        public async Task<bool> EditUserDetail(UserDetails userDetails)
        {
            var existingUserDetails = await _dbCollection.UserDBServices.GetItemById(x => x.Id == userDetails.Id && x.TokenUserId == userDetails.TokenUserId);
            if (existingUserDetails != null)
            {
                existingUserDetails.Address = userDetails.Address;
                existingUserDetails.PhoneNumber = userDetails.PhoneNumber;
                await _dbCollection.UserDBServices.UpdateItem(existingUserDetails);
                _logger.LogInformation("User details updated successfully");
                return true;
            }
            _logger.LogInformation("User details not found");
            return false;
        }

        public async Task<bool> DeleteUserDetail(int userDetailsId, string userId)
        {
            var userDetails = await _dbCollection.UserDBServices.GetItemById(x => x.Id == userDetailsId && x.TokenUserId == userId);
            if (userDetails != null)
            {
                await _dbCollection.UserDBServices.DeleteItem(userDetails);
                _logger.LogInformation("User details deleted successfully");
                return true;
            }
            _logger.LogInformation("User details not found");
            return false;
        }
    }
}
