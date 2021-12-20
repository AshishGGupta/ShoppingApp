namespace ShoppingAppTest.ServicesTest
{
    using Microsoft.Extensions.Logging;
    using Moq;
    using ShoppingApp.DataAccess.IDataAccess;
    using ShoppingApp.Models.Model;
    using ShoppingApp.Services.Services;
    using ShoppingAppTest.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Xunit;

    public class ProductServicesTest
    {
        private readonly Mock<IDbFacade> _dbFacade;
        private readonly Mock<ILogger<ProductServices>> _logger;
        private readonly ProductServices _productServices;
        private readonly GetData _getData;

        public ProductServicesTest()
        {
            _getData = new GetData();
            _dbFacade = new Mock<IDbFacade>();
            _logger = new Mock<ILogger<ProductServices>>();
            _productServices = new ProductServices(_dbFacade.Object, _logger.Object);
        }

        [Fact]
        public async Task GetProductList_Success()
        {
            //Arrange
            _dbFacade.Setup(x => x.ProductDbServices.GetProductList()).ReturnsAsync(_getData.GetProductsData());
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
            _dbFacade.Setup(x => x.ProductDbServices.GetProductList(sortAndFilter)).ReturnsAsync(_getData.GetProductsData());
            //Act
            var result = await _productServices.GetProductList(sortAndFilter);
            //Assert
            Assert.Equal(2, result.Count);
        }

        [Fact]
        public async Task GetProductById_Success()
        {
            //Arrange
            _dbFacade.Setup(x => x.ProductDbServices.GetProductById(1)).ReturnsAsync(_getData.GetProductsData().FirstOrDefault());
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
            _dbFacade.Setup(x => x.ProductDbServices.AddProduct(product));
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
            _dbFacade.Setup(x => x.ProductDbServices.GetProductById(product.ProductId)).ReturnsAsync(product);
            _dbFacade.Setup(x => x.ProductDbServices.UpdateProduct(product));
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
            _dbFacade.Setup(x => x.ProductDbServices.GetProductById(product.ProductId));
            _dbFacade.Setup(x => x.ProductDbServices.UpdateProduct(product));
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
            _dbFacade.Setup(x => x.ProductDbServices.GetProductById(product.ProductId)).ReturnsAsync(product);
            _dbFacade.Setup(x => x.ProductDbServices.DeleteProduct(product));
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
            _dbFacade.Setup(x => x.ProductDbServices.GetProductById(product.ProductId));
            _dbFacade.Setup(x => x.ProductDbServices.DeleteProduct(product));
            //Act
            bool isSuccess = await _productServices.DeleteProduct(product.ProductId);
            //Assert
            Assert.False(isSuccess);
        }
    }
}
