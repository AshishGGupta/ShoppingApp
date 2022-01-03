namespace ShoppingApp.DataAccess.IDataAccess
{
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Models.MediatorClass;
    using System.Threading.Tasks;

    public interface IUserLoginLogoutDbServices
    {
        //Login/signup
        Task RegisterUser(User user);
        Task<string> UserExists(string userName);
        Task<bool> LoginUser(LoginUsersDetails loginUsersDetails);
        Task<bool> LogoutUser(LogoutUser logoutUser);
        Task<bool> SessionExists (string sessionId);
    }
}
