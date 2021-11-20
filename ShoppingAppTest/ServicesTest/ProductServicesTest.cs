namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.IDataAccess;
    using ShoppingApp.Model;
    using ShoppingApp.Services;
    using ShoppingAppTest.Common;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductServicesTest
    {
        private readonly Mock<IDBServices> _dbService;
        private readonly Mock<ILogger<ProductServices>> _logger;
        private readonly ProductServices _productServices;
        private readonly GetData _getData;

        public ProductServicesTest()
        {
            _getData = new GetData();
            _dbService = new Mock<IDBServices>();
            _logger = new Mock<ILogger<ProductServices>>();
            _productServices = new ProductServices(_dbService.Object, _logger.Object);
        }

        [Fact]
        public async Task GetProductList_Success()
        {
            //Arrange
            _dbService.Setup(x => x.GetProductList()).ReturnsAsync(_getData.GetProductsData());
            //Act
            var result = await _productServices.GetProductList();
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProductListSortAndFilter_Success()
        {
            //Arrange
            SortAndFilter sortAndFilter = _getData.GetSortAndFilterData();
            _dbService.Setup(x => x.GetProductList(sortAndFilter)).ReturnsAsync(_getData.GetProductsData());
            //Act
            var result = await _productServices.GetProductList(sortAndFilter);
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProductById_Success()
        {
            //Arrange
            _dbService.Setup(x => x.GetProductById(1)).ReturnsAsync(_getData.GetProductsData().FirstOrDefault());
            //Act
            var result = await _productServices.GetProductById(1);
            //Assert
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task AddProduct_Success()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _dbService.Setup(x => x.AddProduct(product));
            //Act
            var result = await _productServices.AddProduct(product);
            //Assert
            Assert.True(result);
        }

        [Fact]
        public async Task UpdateProduct_Success()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _dbService.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);
            _dbService.Setup(x => x.UpdateProduct(product));
            //Act
            bool isSuccess = await _productServices.UpdateProduct(product);
            //Assert
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task UpdateProduct_NotFound()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _dbService.Setup(x => x.GetProductById(product.ProductId));
            _dbService.Setup(x => x.UpdateProduct(product));
            //Act
            bool isSuccess = await _productServices.UpdateProduct(product);
            //Assert
            Assert.False(isSuccess);
        }

        [Fact]
        public async Task DeleteProduct_Success()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _dbService.Setup(x => x.GetProductById(product.ProductId)).ReturnsAsync(product);
            _dbService.Setup(x => x.DeleteProduct(product));
            //Act
            bool isSuccess = await _productServices.DeleteProduct(product.ProductId);
            //Assert
            Assert.True(isSuccess);
        }

        [Fact]
        public async Task DeleteProduct_NotFound()
        {
            //Arrange
            var product = _getData.GetProductsData().FirstOrDefault();
            _dbService.Setup(x => x.GetProductById(product.ProductId));
            _dbService.Setup(x => x.DeleteProduct(product));
            //Act
            bool isSuccess = await _productServices.DeleteProduct(product.ProductId);
            //Assert
            Assert.False(isSuccess);
        }
    }
}
