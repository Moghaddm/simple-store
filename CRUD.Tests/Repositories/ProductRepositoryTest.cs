using CRUD.Api.Domain;
using CRUD.Api.Repositories;
using CRUD.Tests.Fixtures;
using Microsoft.EntityFrameworkCore;
using Moq;
using Persistence;

namespace CRUD.Tests.Repositories;

public class ProductRepositoryTest
{
    private readonly Mock<StoreContext> _mockContext;
    private readonly ProductRepository _repository;

    public ProductRepositoryTest()
    {
        var optionsBuilder = new DbContextOptionsBuilder<StoreContext>();
        optionsBuilder.UseInMemoryDatabase("StoreDb");

        _mockContext = new Mock<StoreContext>(optionsBuilder.Options);
        _repository = new ProductRepository(_mockContext.Object);
    }

    [Fact]
    public async Task GetProduct_ShouldReturnCorrectProduct_WhenProductExist()
    {
        // Arrange
        int productId = 1;
        var product = new Product("Test Name For Product", "Test Description For Product", 10000)
        {
            Id = productId
        };
        product.GiveRate((int)productId);
        byte[] image = new byte[] { 10, 11, 12, 13, 14 };
        product.AddAttachment(image, "Simple Alt For Photo");
        _mockContext.Setup(c => c.Products.FindAsync(productId)).ReturnsAsync(product);
        // Act
        var result = await _repository.GetProduct(productId);
        // Assert
        Assert.Equal<Product>(product, result);
    }

    [Fact]
    public async Task GetProducts_ShouldReturnAllProducts()
    {
        // Arrange 
        List<Product> products = new List<Product>{
            new Product("Test Name For Product 1", "Test Description For Product 1", 10000) {},
            new Product("Test Name For Product 2", "Test Description For Product 2", 10000) {},
            new Product("Test Name For Product 3", "Test Description For Product 3", 10000) {}
        };
        _mockContext.Setup(c => c.Products.ToList()).Returns(products);

        // Act
        var result = await _repository.GetProducts();

        // Assert
        Assert.Equal(products, result);
    }

    [Fact]
    public async Task DeleteProduct_ShouldRemoveProduct_WhenProductExist()
    {
        // Arrange
        int productId = 1;
        var product = new Product("Test Name For Product", "Test Description For Product", 10000)
        {
            Id = productId
        };
        product.GiveRate((int)productId);
        byte[] image = new byte[] { 10, 11, 12, 13, 14 };
        product.AddAttachment(image, "Simple Alt For Photo");
        _mockContext.Setup(c => c.Products.FindAsync(productId)).ReturnsAsync(product);

        // Act
        await _repository.DeleteProduct(productId);

        // Assert
        _mockContext.Verify(c => c.Products.Remove(product), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }
    [Fact]
    public async Task InserProduct_ShouldAddedToDatabase()
    {
        // Arrange 
        int productId = 1;
        var product = new Product("Test Name For Product", "Test Description For Product", 10000)
        {
            Id = productId
        };
        product.GiveRate((int)productId);
        byte[] image = new byte[3];
        product.AddAttachment(image, "Simple Alt For Photo");

        // Act 
        await _repository.InsertProduct(product);
        // Assert
        _mockContext.Verify(c => c.AddAsync(product, default), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }

    [Fact]
    public async Task UpdateProduct_ShouldUptoDataProduct()
    {
        // Arrange
        int productId = 1;
        var product = new Product("Test Name For Product", "Test Description For Product", 10000)
        {
            Id = productId
        };
        product.GiveRate((int)productId);
        byte[] image = new byte[3];
        product.AddAttachment(image, "Simple Alt For Photo");


        var productUpdated = new Product("Test Name For Product", "Test Description For Product", 10000)
        {
            Id = productId
        };
        productUpdated.GiveRate((int)productId);
        productUpdated.AddAttachment(image, "Simple Alt For Photo");
        _mockContext.Setup(c => c.Products.FindAsync(productId)).ReturnsAsync(productUpdated);

        // Act
        await _repository.UpdateProduct(productId, productUpdated);

        // Assert
        Assert.Equal(product.Name, productUpdated.Name);
        Assert.Equal(product.Description, productUpdated.Description);
        Assert.Equal(product.Price, productUpdated.Price);
        Assert.Equal(product.Rate, productUpdated.Rate);
        Assert.Equal(product.Attachments, productUpdated.Attachments);
        _mockContext.Verify(c => c.Products.Update(product), Times.Once);
        _mockContext.Verify(c => c.SaveChanges(), Times.Once);
    }
}
