using CRUD.Api.Controllers;
using Xunit;
using Moq;
using CRUD.Api.Repositories;
using Persistence;
using CRUD.Api.Domain;

namespace CRUD.Tests.Repositories;

public class ProductRepositoryTest
{
    private readonly Mock<StoreContext> _mockContext;
    private readonly ProductRepository _repository;

    public ProductRepositoryTest(Mock<StoreContext> context, ProductRepository repository) =>
        (_mockContext, _repository) = (context, repository);

    [Fact]
    public async Task GetProduct_ShouldReturnCorrectProduct_WhenProductExist()
    {
        // Arrange
        int productId = 1;
        var product = new Product
        {
            Id = productId,
            Name = "Test Name For Product",
            Description = "Test Description For Product",
            Price = 10000,
            Rate = 4,
            Attachments = new List<Attachment>
            {
                new Attachment { Alt = "Simple Alt For Photo", Image = null }
            }
        };
        _mockContext.Setup(c => c.Products.FindAsync(productId)).ReturnsAsync(product);
        // Act
        var result = await _repository.GetProduct(productId);
        // Assert
        Assert.Equal<Product>(product, result);
    }
}
