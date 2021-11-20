namespace ShoppingApp.IDataAccess
{
    using ShoppingApp.Model;
    using ShoppingApp.Model.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IDBServices
    {
        //Login/signup
        Task RegisterUser(User user);
        Task<bool> UserExists(string userName);

        //product
        Task AddProduct(Product product);
        Task<List<Product>> GetProductList();
        Task<List<Product>> GetProductList(SortAndFilter sortFilter);
        Task<Product> GetProductById(int productId);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<bool> ProductExists(int productId);
    }
}
