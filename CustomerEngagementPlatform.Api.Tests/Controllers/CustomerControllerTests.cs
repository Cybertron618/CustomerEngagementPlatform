using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using CustomerEngagementPlatform.Api.src.Controllers;
using CustomerEngagementPlatform.Api.src.Models;
using CustomerEngagementPlatform.Api.src.Services;

namespace CustomerEngagementPlatform.Api.Tests.Controllers
{
    public class CustomerControllerTests
    {
        private readonly Mock<ICustomerService> _mockCustomerService;
        private readonly CustomersController _controller;

        public CustomerControllerTests()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockCustomerService.Object);
        }

        [Fact]
        public async Task GetAllCustomers_ReturnsOkResult_WithListOfCustomers()
        {
            // Arrange
            var mockCustomers = new List<Customer>
            {
                new() { CustomerId = 1, Name = "John Doe" },
                new() { CustomerId = 2, Name = "Jane Doe" }
            };

            _mockCustomerService.Setup(service => service.GetAllCustomersAsync())
                .ReturnsAsync(mockCustomers);

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<List<Customer>>(okResult.Value);
            Assert.Equal(2, returnValue.Count);
        }

        [Fact]
        public async Task GetCustomerById_ReturnsOkResult_WithCustomer()
        {
            // Arrange
            var customerId = 1;
            var mockCustomer = new Customer { CustomerId = customerId, Name = "John Doe" };

            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(mockCustomer);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnValue = Assert.IsType<Customer>(okResult.Value);
            Assert.Equal(customerId, returnValue.CustomerId);
        }

        [Fact]
        public async Task AddCustomer_ReturnsCreatedAtActionResult_WithCustomer()
        {
            // Arrange
            var mockCustomer = new Customer { CustomerId = 1, Name = "John Doe" };

            _mockCustomerService.Setup(service => service.AddCustomerAsync(mockCustomer))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.AddCustomer(mockCustomer);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<Customer>(createdAtActionResult.Value);
            Assert.Equal(mockCustomer.CustomerId, returnValue.CustomerId);
        }

        [Fact]
        public async Task UpdateCustomer_ReturnsNoContentResult_WhenSuccessful()
        {
            // Arrange
            var customerId = 1;
            var mockCustomer = new Customer { CustomerId = customerId, Name = "John Doe" };

            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(mockCustomer);

            _mockCustomerService.Setup(service => service.UpdateCustomerAsync(mockCustomer))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateCustomer(customerId, mockCustomer);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task DeleteCustomer_ReturnsNoContentResult_WhenSuccessful()
        {
            // Arrange
            var customerId = 1;
            var mockCustomer = new Customer { CustomerId = customerId, Name = "John Doe" };

            _mockCustomerService.Setup(service => service.GetCustomerByIdAsync(customerId))
                .ReturnsAsync(mockCustomer);

            _mockCustomerService.Setup(service => service.DeleteCustomerAsync(customerId))
                .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCustomer(customerId);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
