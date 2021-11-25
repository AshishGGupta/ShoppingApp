﻿namespace ShoppingApp.DataAccess.IDataAccess
{
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
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
        Task<List<Product>> GetProductByListOfId(List<string> productIdList);
        Task UpdateProduct(Product product);
        Task DeleteProduct(Product product);
        Task<bool> ProductExists(int productId);
    }
}