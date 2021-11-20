using ShoppingApp.Model.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShoppingApp.Interfaces
{
    public interface IUserDetailServices
    {
        Task<List<UserDetails>> GetUserDetails(string userId);
        Task AddUserDetail(UserDetails userDetails);
        Task<bool> EditUserDetail(UserDetails userDetails);
        Task<bool> DeleteUserDetail(int userDetailsId, string userId);
    }
}
