using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerEngagementPlatform.Api.src.Models;
using CustomerEngagementPlatform.Api.src.Repositories;
using CustomerEngagementPlatform.Api.src.Services;
using Moq;
using Xunit;

namespace CustomerEngagementPlatform.Api.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly Mock<ICustomerRepository> _mockRepo;
        private readonly CustomerService _service;

        public CustomerServiceTests()
        {
            _mockRepo = new Mock<ICustomerRepository>();
            _service = new CustomerService(_mockRepo.Object);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ReturnsListOfCustomers()
        {
            // Arrange
            var customers = new List<Customer> { new() { CustomerId = 1, Name = "John Doe" } };
            _mockRepo.Setup(repo => repo.GetAllCustomersAsync()).ReturnsAsync(customers);

            // Act
            var result = await _service.GetAllCustomersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsCustomer()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };
            _mockRepo.Setup(repo => repo.GetCustomerByIdAsync(1)).ReturnsAsync(customer);

            // Act
            var result = await _service.GetCustomerByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customer.CustomerId, result.CustomerId);
        }

        [Fact]
        public async Task AddCustomerAsync_CallsRepository()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };

            // Act
            await _service.AddCustomerAsync(customer);

            // Assert
            _mockRepo.Verify(repo => repo.AddCustomerAsync(customer), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_CallsRepository()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };

            // Act
            await _service.UpdateCustomerAsync(customer);

            // Assert
            _mockRepo.Verify(repo => repo.UpdateCustomerAsync(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_CallsRepository()
        {
            // Arrange
            var customer = new Customer { CustomerId = 1, Name = "John Doe" };

            // Act
            await _service.DeleteCustomerAsync(1);

            // Assert
            _mockRepo.Verify(repo => repo.DeleteCustomerAsync(1), Times.Once);
        }
    }
}
