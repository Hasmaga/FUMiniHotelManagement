using Model.RepDto;
using Model.ResDto;

namespace Service.Interface
{
    public interface ICustomerService
    {
        Task<string> LoginAsync(LoginResDto loginResDto);
        Task<bool> CreateCustomerAsync(CreateCustomerResDto createCustomerResDto);
        Task<List<GetCustomerResDto>> GetAllCustomerAsync();
        Task<bool> UpdateCustomerAsync(UpdateCustomerResDto updateCustomerResDto);
        Task<bool> DeleteCustomerAsync(int customerId);
        Task<bool> UpdateProfileByCustomerAsync(UpdateProfileByCustomerResDto updateProfileByCustomerResDto);
    }
}
