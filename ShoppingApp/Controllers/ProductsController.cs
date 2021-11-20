namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Common;
    using ShoppingApp.Interfaces;
    using ShoppingApp.Model;
    using ShoppingApp.Model.Domain;
    using ShoppingApp.Validation;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [ApiController]
    [ServiceFilter(typeof(ExceptionAttribute))]
    public class ProductsController : Controller
    {
        private readonly IProductServices _productServices;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(IProductServices productServices, ILogger<ProductsController> logger)
        {
            _productServices = productServices; 
            _logger = logger;
        }

        [HttpGet("product/GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            _logger.LogInformation("Entering GetProductList");
            List<Product> productList = await _productServices.GetProductList();
            _logger.LogInformation("Product found Count:" + productList?.Count);
            if (productList?.Count > 0)
                return Ok(productList);
            return NotFound("No Products Found");
        }

        [HttpPost("product/GetProductList")]
        public async Task<IActionResult> GetProductList([FromBody] SortAndFilter sortFilter)
        {
            _logger.LogInformation("Entering GetProductList Post method");
            List<Product> productList = await _productServices.GetProductList(sortFilter);
            _logger.LogInformation("Product found Count:" + productList?.Count);
            if (productList?.Count > 0)
                return Ok(productList);
            return NotFound("No Products Found");
        }

        [HttpGet("product/GetProductById/{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            _logger.LogInformation("Entering GetProductById method");
            Product product = await _productServices.GetProductById(id);
            if (product != null)
            {
                _logger.LogInformation("Product found Successfully. productId:" + product.ProductId);
                return Ok(product);
            }
            return NotFound("Product not found");
        }

        [Authorize]
        [HttpPost("Product/AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            _logger.LogInformation("Entering AddProduct method");
            string permissionValue = User.Claims.FirstOrDefault(c => c.Type == Constants.Permission)?.Value;
            if (permissionValue != Constants.PermissionValue)
            {
                _logger.LogInformation("User do not have permission to perform this operation.");
                return Unauthorized("User do not have permission to perform this operation.");
            }
            await _productServices.AddProduct(product);
            return Ok("Product Added Successfully");
        }
        
        [Authorize]
        [HttpPut("Product/UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            _logger.LogInformation("Entering UpdateProduct method");
            string permissionValue = User.Claims.FirstOrDefault(c => c.Type == Constants.Permission)?.Value;
            if (permissionValue != Constants.PermissionValue)
            {
                _logger.LogInformation("User do not have permission to perform this operation.");
                return Unauthorized("User do not have permission to perform this operation.");
            }
            var isSuccess = await _productServices.UpdateProduct(product);
            if (isSuccess)
                return Ok("Product Updated Successfully");
            return NotFound("Product not Updated");
        }

        [Authorize]
        [HttpDelete("product/DeleteProduct/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            _logger.LogInformation("Entering DeleteProduct method");
            string permissionValue = User.Claims.FirstOrDefault(c => c.Type == Constants.Permission)?.Value;
            if (permissionValue != Constants.PermissionValue)
            {
                _logger.LogInformation("User do not have permission to perform this operation.");
                return Unauthorized("User do not have permission to perform this operation.");
            }
            bool isDeleted = await _productServices.DeleteProduct(id);
            if (isDeleted)
                return Ok("Product Deleted Successfully");
            return NotFound("Product not found");
        }
    }
}
