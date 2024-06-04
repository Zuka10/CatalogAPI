using AutoMapper;
using Catalog.gRPCService;
using Ecommerce.DTO;
using Ecommerce.Repository;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Catalog.Test;

public class gRPCTest
{
    private CatalogDbContext _dbContext;
    private Mock<IMapper> _mockMapper;

    [SetUp]
    public void Setup()
    {
        var options = new DbContextOptionsBuilder<CatalogDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _dbContext = new CatalogDbContext(options);
        _mockMapper = new Mock<IMapper>();
    }

    [Test]
    public async Task AddCategory_ValidCategory_ReturnsIdRequest()
    {
        // Arrange
        var service = new CategorygRPCService(_dbContext);
        var categoryDto = new CategoryDTO { Id = 1, Name = "test" };
        var idRequest = new IdRequest { Id = 1 };

        // Act
        var result = await service.AddCategory(categoryDto, It.IsAny<ServerCallContext>());

        // Assert
        Assert.That(result.Id, Is.EqualTo(idRequest.Id));
    }

    [TearDown]
    public void TearDown()
    {
        _dbContext.Dispose();
    }
}