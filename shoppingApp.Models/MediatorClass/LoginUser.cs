namespace ShoppingApp.Models.MediatorClass
{
    using MediatR;
    using ShoppingApp.Models.Model;

    public class LoginUser : UserModel, IRequest<ApiResponse>
    {
    }
}
