using Model.Models;

namespace Repository.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer?> GetCustomerByEmailAsync(string email);
        Task<bool> CreateCustomerAsync(Customer customer);
        Task<List<Customer>> GetAllCustomerAsync();
        Task<Customer?> GetCustomerByIdAsync(int id);
        Task<bool> UpdateCustomerAsync(Customer customer);
        Task<bool> DeleteCustomerAsync(Customer customer);
    }
}
