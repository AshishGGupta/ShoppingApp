namespace ShoppingApp.DataAccess.IDataAccess
{
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface ICartDbService: IDbOperations<Cart>
    {
        Task<List<Cart>> GetCartDetails(string userId);
        Task BulkCartDelete(List<Cart> cart);
        Task<Cart> CartItemExistsByProductId(int productId, string userId);
    }
}
