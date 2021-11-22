namespace ShoppingApp.Services.IServices
{
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using System.Threading.Tasks;

    public interface ICartServices
    {
        Task<CartResponse> GetCartDetails(string userId);
        Task AddToCart(Cart cart);
        Task<bool> Edit(Cart cart);
        Task<bool> Delete(int cartId);
    }
}
