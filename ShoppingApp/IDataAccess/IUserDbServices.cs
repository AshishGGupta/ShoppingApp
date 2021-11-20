namespace ShoppingApp.IDataAccess
{
    using ShoppingApp.Model.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserDbServices
    {
        Task<List<UserDetails>> GetUserDetails(string userId);
        Task AddUserDetails(UserDetails userDetails);
        Task UpdateUserDetails(UserDetails userDetails);
        Task DeleteUserDetail(UserDetails userDetails);
        Task<UserDetails> UserItemExists(int userDetailsId, string userId);
    }
}
