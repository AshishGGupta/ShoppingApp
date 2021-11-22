namespace ShoppingApp.Services.Services
{
    using Microsoft.Extensions.Logging;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ProductServices : IProductServices
    {
        private readonly IDBServices _dBServices;
        private readonly ILogger<ProductServices> _logger;

        public ProductServices(IDBServices dBServices, ILogger<ProductServices> logger)
        {
            _dBServices = dBServices;
            _logger = logger;
        }
        
        public async Task<List<Product>> GetProductList()
        {
            return await _dBServices.GetProductList();
        }

        public async Task<List<Product>> GetProductList(SortAndFilter sortFilter)
        {
            return await _dBServices.GetProductList(sortFilter);
        }

        public async Task<bool> AddProduct(Product product)
        {
            await _dBServices.AddProduct(product);
            _logger.LogInformation("Product Added Successfully");
            return true;
        }

        public async Task<Product> GetProductById(int productId)
        {
            return await _dBServices.GetProductById(productId);
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            var existingProduct = await _dBServices.GetProductById(product.ProductId);
            if (existingProduct != null)
            {
                existingProduct.Name = product.Name;
                existingProduct.Description = product.Description;
                existingProduct.Category = product.Category;
                existingProduct.Price = product.Price;
                existingProduct.DateAdded = product.DateAdded;
                existingProduct.ExpiryDate = product.ExpiryDate;
                await _dBServices.UpdateProduct(existingProduct);
                _logger.LogInformation("Product updated successfully");
                return true;
            }
            _logger.LogInformation("Product not found. productId: " + product.ProductId);
            return false;
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var existingProduct = await _dBServices.GetProductById(productId);
            if (existingProduct != null)
            {
                await _dBServices.DeleteProduct(existingProduct);
                _logger.LogInformation("Product delected successfully");
                return true;
            }
            _logger.LogInformation("Product not found. productId: " + productId);
            return false;
        }
    }
}
