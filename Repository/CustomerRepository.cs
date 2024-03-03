using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Interface;

namespace Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public CustomerRepository(FuminiHotelManagementContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateCustomerAsync(Customer customer)
        {
            try
            {
                await _db.Customers.AddAsync(customer);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteCustomerAsync(Customer customer)
        {
            try
            {
                _db.Customers.Remove(customer);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Customer>> GetAllCustomerAsync()
        {
            try
            {
                return await _db.Customers.ToListAsync(); 
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer?> GetCustomerByEmailAsync(string email)
        {
            try
            {
                return await _db.Customers.FirstOrDefaultAsync(x => x.EmailAddress == email);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<Customer?> GetCustomerByIdAsync(int id)
        {
            try
            {
                return await _db.Customers.FindAsync(id);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateCustomerAsync(Customer customer)
        {
            try
            {
                _db.Customers.Update(customer);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        } 
    }
}
