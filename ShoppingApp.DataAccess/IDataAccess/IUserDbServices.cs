namespace ShoppingApp.DataAccess.IDataAccess
{
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IUserDbServices: IDbOperations<UserDetails>
    {
        Task<List<UserDetails>> GetUserDetails(string userId);
    }
}
