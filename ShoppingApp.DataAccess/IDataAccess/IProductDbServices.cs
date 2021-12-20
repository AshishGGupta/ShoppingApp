namespace ShoppingApp.DataAccess.IDataAccess
{
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductDbServices : IDbOperations<Product>
    {
        //Login/signup
        Task RegisterUser(User user);
        Task<bool> UserExists(string userName);

        //product
        Task<List<Product>> GetProductList(SortAndFilter sortFilter);
        Task<List<Product>> GetProductByListOfId(List<string> productIdList);
    }
}
