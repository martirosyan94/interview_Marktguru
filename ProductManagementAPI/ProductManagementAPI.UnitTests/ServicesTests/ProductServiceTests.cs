using Moq;
using ProductManagementAPI.Data;
using ProductManagementAPI.Data.Models;
using ProductManagementAPI.Data.RepositoryInterfaces;
using ProductManagementAPI.Services.Models.Request;
using ProductManagementAPI.Services.Services;

namespace ProductManagementAPI.UnitTests.ServicesTests
{
    [TestFixture]
    public class ProductServiceTests
    {
        private ProductService _productService;
        private Mock<IProductRepository> _productRepositoryMock;
        private readonly CancellationToken _cancellationToken = CancellationToken.None;

        [SetUp]
        public void Setup()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _productService = new ProductService(_productRepositoryMock.Object);
        }

        [Test]
        public async Task AddProductAsync_ShouldReturnSuccess_WhenNewProductIsAdded()
        {
            const string name = "Laptop";
            var newProductDto = new PostProductDto { Name = name };
            _productRepositoryMock.Setup(repo => repo.GetProductByNameAsync(newProductDto.Name, _cancellationToken))
                .ReturnsAsync(null as Product);
            _productRepositoryMock.Setup(repo => repo.AddProductAsync(It.Is<Product>(e => e.Name == name), _cancellationToken))
                .Returns(Task.CompletedTask);
            _productRepositoryMock.Setup(repo => repo.SaveChangesAsync(_cancellationToken))
                .ReturnsAsync(true);

            var result = await _productService.AddProductAsync(newProductDto, _cancellationToken);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(Status.Created, result.Status);

            _productRepositoryMock.Verify(repo => repo.GetProductByNameAsync(newProductDto.Name, _cancellationToken), times: Times.Once);
            _productRepositoryMock.Verify(repo =>
                repo.AddProductAsync(It.Is<Product>(e => e.Name == name), _cancellationToken), times: Times.Once);
            _productRepositoryMock.Verify(repo => repo.SaveChangesAsync(_cancellationToken), times: Times.Once);
        }

        [Test]
        public async Task AddProductAsync_ShouldReturnError_WhenProductIsAlreadyInCatalog()
        {
            const string name = "Keyboard";
            var existingProductDto = new PostProductDto { Name = name };
            _productRepositoryMock.Setup(repo => repo.GetProductByNameAsync(existingProductDto.Name, _cancellationToken))
                .ReturnsAsync(new Product { Name = name });

            var result = await _productService.AddProductAsync(existingProductDto, _cancellationToken);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(Status.BadRequest, result.Status);
            Assert.AreEqual("This product already exists", result.ErrorMessage);

            _productRepositoryMock.Verify(repo => repo.GetProductByNameAsync(existingProductDto.Name, _cancellationToken), times: Times.Once);
        }

        [Test]
        public async Task AddProductAsync_ShouldReturnError_WhenSaveChangesFails()
        {
            var productDto = new PostProductDto { Name = "Keyboard" };

            _productRepositoryMock.Setup(repo => repo.SaveChangesAsync(_cancellationToken))
                .ReturnsAsync(false);

            var result = await _productService.AddProductAsync(productDto, _cancellationToken);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(Status.NetworkError, result.Status);
            Assert.AreEqual("Failed to add the product.", result.ErrorMessage);

            _productRepositoryMock.Verify(repo => repo.SaveChangesAsync(_cancellationToken), times: Times.Once);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnAllAvailableProducts_WhenAtLeastOneProductExists()
        {
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Monitor" },
                new Product { Id = 2, Name = "Mouse" }
            };
            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync(_cancellationToken))
                .ReturnsAsync(products);

            var result = await _productService.GetAllProductsAsync(_cancellationToken);

            Assert.IsTrue(result.Success);
            Assert.IsNotEmpty(result.Data);

            _productRepositoryMock.Verify(repo => repo.GetAllProductsAsync(_cancellationToken), times: Times.Once);
        }

        [Test]
        public async Task GetAllProductsAsync_ShouldReturnError_WhenNoProductsFound()
        {
            _productRepositoryMock.Setup(repo => repo.GetAllProductsAsync(_cancellationToken))
                .ReturnsAsync(new List<Product>());

            var result = await _productService.GetAllProductsAsync(_cancellationToken);

            Assert.IsFalse(result.Success);
            Assert.AreEqual(Status.NotFound, result.Status);
            Assert.AreEqual("There are no available products", result.ErrorMessage);

            _productRepositoryMock.Verify(repo => repo.GetAllProductsAsync(_cancellationToken), times: Times.Once);
        }
    }
}
