using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using webapi_docker.Commands;
using webapi_docker.Entities;
using webapi_docker.Interfaces;
using Moq;
using Xunit;
using Microsoft.EntityFrameworkCore;

namespace mediator_api.Test.Commands
{
    public class CreateProductCommandTests
    {
        private readonly Mock<IApiDbContext> _mockContext;
        private readonly CreateProductCommand.CreateProductCommandHandler _handler;

        public CreateProductCommandTests()
        {
            _mockContext = new Mock<IApiDbContext>();
            _handler = new CreateProductCommand.CreateProductCommandHandler(_mockContext.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateProduct_WhenValidCommand()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 9.99
            };

            var products = new List<Product>().AsQueryable();
            var mockSet = new Mock<DbSet<Product>>();
            mockSet.As<IQueryable<Product>>().Setup(m => m.Provider).Returns(products.Provider);
            mockSet.As<IQueryable<Product>>().Setup(m => m.Expression).Returns(products.Expression);
            mockSet.As<IQueryable<Product>>().Setup(m => m.ElementType).Returns(products.ElementType);
            mockSet.As<IQueryable<Product>>().Setup(m => m.GetEnumerator()).Returns(products.GetEnumerator());

            _mockContext.Setup(x => x.Products).Returns(mockSet.Object);
            _mockContext.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.Price.Should().Be(command.Price);

            _mockContext.Verify(x => x.Products.Add(It.IsAny<Product>()), Times.Once);
            _mockContext.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_ShouldThrowException_WhenContextFails()
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = "Test Product",
                Price = 9.99
            };

            _mockContext.Setup(x => x.SaveChangesAsync())
                       .ThrowsAsync(new Exception("Database error"));

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => 
                _handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData("", 9.99)]
        [InlineData("Name", -1)]
        public async Task Handle_ShouldThrowException_WhenInvalidCommand(
            string name, double price)
        {
            // Arrange
            var command = new CreateProductCommand
            {
                Name = name,
                Price = price
            };

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() =>
                _handler.Handle(command, CancellationToken.None));
        }
    }
}
