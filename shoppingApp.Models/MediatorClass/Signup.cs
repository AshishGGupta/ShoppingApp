namespace ShoppingApp.Models.MediatorClass
{
    using MediatR;
    using ShoppingApp.Models.Model;

    public class Signup : UserModel, IRequest<ApiResponse>
    {

    }
}
