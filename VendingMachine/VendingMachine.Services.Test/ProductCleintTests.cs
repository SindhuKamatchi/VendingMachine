using VendingMachine.Services.Interface;
using Moq;
using VendingMachine.Services.Test.Helpers;
using VendingMachine.Services.Models;
using VendingMachine.Services.Enum;
using Xunit;
using Moq.Protected;
using System.Net;


namespace VendingMachine.Services.Test
{
    public class ProductCleintTests
    {

        private readonly Mock<HttpMessageHandler> _mockMessageHandler;
        private Mock<IProductRepository> _productRepository;
        private Mock<IProductInventoryRepository> _productInventoryRepository;
        private Mock<IProductService> _underTest;

        public ProductCleintTests()
        {

            _mockMessageHandler = new Mock<HttpMessageHandler>();
            _productRepository = new Mock<IProductRepository>();
            _productInventoryRepository = new Mock<IProductInventoryRepository>();
       
            var client = _mockMessageHandler.CreateClient();
            _underTest = new WeatherClient(client, _mockConfig.Object, _mockLogger.Object);
        }
        [TestMethod]
        public void ProductService_ProductRepository_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            AssertException.Throws<ArgumentNullException>(() => new _productService(null, _productInventoryRepository.Object), "Value cannot be null.\r\nParameter name: productRepository parameter is null");
        }

        [TestMethod]
        public void ProductService_ProductInventoryRepository_IsNull_ExceptionThrown()
        {
            // Arrange + Act + Assert
            AssertException.Throws<ArgumentNullException>(() => new _productService(_productRepository.Object, null), "Value cannot be null.\r\nParameter name: productInventoryRepository parameter is null");
        }

        [TestMethod]
        public void ProductService_GetProduct_IsValidProductReturned()
        {
            // Arrange
            _productRepository.Setup(mock => mock.GetProductList()).Returns(CreateProducts());

            // Act
            var productService = new _productService(_productRepository.Object, _productInventoryRepository.Object);

            var result = productService.GetProduct("COKE1");

            // Assert
            Assert.AreEqual(result != null, true);
            Assert.AreEqual(result.Code, "COKE1");
            Assert.AreEqual(result.Name, "Coke");
            Assert.AreEqual(result.Price, 1.00m);
            Assert.AreEqual(result.Type, ProductItemType.Cola);
        }

        [TestMethod]
        public void ProductService_GetProductQuantity_IsValidProductQuantityReturned()
        {
            // Arrange
            _productInventoryRepository.Setup(mock => mock.GetInventory()).Returns(CreateProductInventory());

            // Act
            var productService = new _productService(_productRepository.Object, _productInventoryRepository.Object);

            var result = productService.GetProductQuantity("COKE1");

            // Assert
            Assert.AreEqual(result, 2);
        }


        private List<Product> CreateProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Code = "COKE1",
                    Name = "Coke",
                    Price = 1.00m,
                    Type = ProductItemType.Cola
                }
            };
        }

        private Dictionary<string, int> CreateProductInventory()
        {
            return new Dictionary<string, int> { { "COKE1", 2 }, { "PEPSI1", 0 }, { "FANTA1", 10 }, { "SPRITE1", 10 } };
        }

    }

}