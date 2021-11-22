namespace ShoppingApp.Services.IServices
{
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserDetailServices
    {
        Task<List<UserDetails>> GetUserDetails(string userId);
        Task AddUserDetail(UserDetails userDetails);
        Task<bool> EditUserDetail(UserDetails userDetails);
        Task<bool> DeleteUserDetail(int userDetailsId, string userId);
    }
}
