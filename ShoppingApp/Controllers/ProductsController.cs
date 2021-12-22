namespace ShoppingApp.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using ShoppingApp.Common;
    using ShoppingApp.Services.IServices;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Models.Domain;
    using ShoppingApp.Services.Validation;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    [Authorize]
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

        /// <summary>
        /// Get list of all products
        /// </summary>
        /// <returns>List of all the productss</returns>
        /// <response code="200">Returned product detials</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Product Details not found</response>
        [AllowAnonymous]
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

        /// <summary>
        /// Get the product list based on sortby and filter details
        /// </summary>
        /// <param name="sortFilter">sort and filter conditions</param>
        /// <returns>List of products according to sort and filter request</returns>
        /// <response code="200">Returned product detials</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Product Details not found</response>
        [AllowAnonymous]
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

        /// <summary>
        /// Get product by the productId
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns>Product searched based on product ID</returns>
        /// <response code="200">Returned product detials</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Product Details not found</response>
        [AllowAnonymous]
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

        /// <summary>
        /// Add new product
        /// </summary>
        /// <param name="product">Product related details</param>
        /// <returns>status code and message of response</returns>
        /// <response code="200">New Product added successfully</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Product Details not found</response>
        /// <response code="400">Some input value is not valid</response>
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

        /// <summary>
        /// Update the product details
        /// </summary>
        /// <param name="product">Product related details</param>
        /// <returns>status code and message of response</returns>
        /// <response code="200">Product updated successfully</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Product Details not found</response>
        /// <response code="400">Some input value is not valid</response>
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

        /// <summary>
        /// Delete a product
        /// </summary>
        /// <param name="id">product Id to be deleted</param>
        /// <returns>status code and message of response</returns>
        /// <response code="200">Product deleted succsefully</response>
        /// <response code="401">Unauthorized user</response>
        /// <response code="404">Product Details not found</response>
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
