namespace ShoppingAppTest.ControllerTest
{
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.Controllers;
    using ShoppingApp.Services.IServices;
    using ShoppingAppTest.Common;
    using System.Linq;
    using System.Security.Claims;
    using Xunit;

    public class ProductsControllerTest
    {
        private readonly ProductsController _productController;
        private readonly ProductsController _unAuthproductController;
        private readonly Mock<IProductServices> _productServices;
        private readonly Mock<ILogger<ProductsController>> _logger;
        private readonly GetData _getData;
        private readonly string userId = "user123";

        public ProductsControllerTest()
        {
            _getData = new GetData();
            _productServices = new Mock<IProductServices>();
            _logger = new Mock<ILogger<ProductsController>>();

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, userId),
                                        new Claim(ClaimTypes.Name, "user123@gmail.com"),
                                        new Claim("permissions", "All")
                                        // other required and custom claims
                                   }, "TestAuthentication"));
            _productController = new ProductsController(_productServices.Object, _logger.Object);
            _productController.ControllerContext = new ControllerContext();
            _productController.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var unAuthUser = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, userId),
                                        new Claim(ClaimTypes.Name, "user123@gmail.com"),
                                   }, "TestAuthentication"));
            _unAuthproductController = new ProductsController(_productServices.Object, _logger.Object);
            _unAuthproductController.ControllerContext = new ControllerContext();
            _unAuthproductController.ControllerContext.HttpContext = new DefaultHttpContext { User = unAuthUser };
        }

        [Fact]
        public void GetProductList_Success()
        {
            //Arrange
            _productServices.Setup(x => x.GetProductList()).ReturnsAsync(_getData.GetProductsData());
            //Act
            var result = _productController.GetProductList().Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetProductList_NoProductFound()
        {
            //Arrange
            _productServices.Setup(x => x.GetProductList());
            //Act
            var result = _productController.GetProductList().Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetProductListSortAndFilter_Success()
        {
            //Arrange
            var sortAndFilter = _getData.GetSortAndFilterData();
            _productServices.Setup(x => x.GetProductList(sortAndFilter)).ReturnsAsync(_getData.GetProductsData());
            //Act
            var result = _productController.GetProductList(sortAndFilter).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetProductListSortAndFilter_NoProductFound()
        {
            //Arrange
            var sortAndFilter = _getData.GetSortAndFilterData();
            _productServices.Setup(x => x.GetProductList(sortAndFilter));
            //Act
            var result = _productController.GetProductList(sortAndFilter).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetProductListById_Success()
        {
            //Arrange
            _productServices.Setup(x => x.GetProductById(1)).ReturnsAsync(_getData.GetProductsData().FirstOrDefault());
            //Act
            var result = _productController.GetProductById(1).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void GetProductListById_NoProductFound()
        {
            //Arrange
            _productServices.Setup(x => x.GetProductById(1));
            //Act
            var result = _productController.GetProductById(1).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void AddProduct_NoPermmionsProvided()
        {
            //Act
            var result = _unAuthproductController.AddProduct(_getData.GetProductsData().FirstOrDefault()).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void AddProduct_Success()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _productServices.Setup(x => x.AddProduct(product));
            //Act
            var result = _productController.AddProduct(product).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateProduct_NoPermmionsProvided()
        {
            //Act
            var result = _unAuthproductController.UpdateProduct(_getData.GetProductsData().FirstOrDefault()).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateProduct_Success()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _productServices.Setup(x => x.UpdateProduct(product)).ReturnsAsync(true);
            //Act
            var result = _productController.UpdateProduct(product).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void UpdateProduct_ProductNotFound()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _productServices.Setup(x => x.UpdateProduct(product)).ReturnsAsync(false);
            //Act
            var result = _productController.UpdateProduct(product).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteProduct_NoPermmionsProvided()
        {
            //Act
            var result = _unAuthproductController.DeleteProduct(1).Result;
            //Assert
            Assert.Equal(401, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteProduct_Success()
        {
            //Arrange
            _productServices.Setup(x => x.DeleteProduct(1)).ReturnsAsync(true);
            //Act
            var result = _productController.DeleteProduct(1).Result;
            //Assert
            Assert.Equal(200, (result as ObjectResult).StatusCode);
        }

        [Fact]
        public void DeleteProduct_ProductNotFound()
        {
            //Arrange
            _productServices.Setup(x => x.DeleteProduct(1)).ReturnsAsync(false);
            //Act
            var result = _productController.DeleteProduct(1).Result;
            //Assert
            Assert.Equal(404, (result as ObjectResult).StatusCode);
        }
    }
}
