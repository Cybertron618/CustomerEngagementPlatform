using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerEngagementPlatform.Api.src.Models;

namespace CustomerEngagementPlatform.Api.src.Services
{
    public interface ICustomerService
    {
        Task<IEnumerable<Customer>> GetAllCustomersAsync();
        Task<Customer?> GetCustomerByIdAsync(int customerId);
        Task AddCustomerAsync(Customer customer);
        Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomerAsync(int customerId);
    }
}

