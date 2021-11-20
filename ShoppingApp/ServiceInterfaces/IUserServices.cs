namespace ShoppingApp.Interfaces
{
    using ShoppingApp.Model;
    using System.Threading.Tasks;

    public interface IUserServices
    {
        Task<ApiResponse> UserSignUp(UserModel userData);
        Task<ApiResponse> UserLogin(UserModel userData);
    }
}
