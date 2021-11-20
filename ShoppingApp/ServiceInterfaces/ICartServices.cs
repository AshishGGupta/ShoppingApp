namespace ShoppingApp.Interfaces
{
    using ShoppingApp.Model;
    using ShoppingApp.Model.Domain;
    using System.Threading.Tasks;

    public interface ICartServices
    {
        Task<CartResponse> GetCartDetails(string userId);
        Task AddToCart(Cart cart);
        Task<bool> Edit(Cart cart);
        Task<bool> Delete(int cartId);
    }
}
