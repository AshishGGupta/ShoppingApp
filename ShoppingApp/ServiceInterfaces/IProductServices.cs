namespace ShoppingApp.Interfaces
{
    using ShoppingApp.Model;
    using ShoppingApp.Model.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductServices
    {
        Task<List<Product>> GetProductList();
        Task<Product> GetProductById(int productId);
        Task<bool> AddProduct(Product product);
        Task<bool> UpdateProduct(Product product);
        Task<bool> DeleteProduct(int productId);
        Task<List<Product>> GetProductList(SortAndFilter sortFilter);
    }
}
