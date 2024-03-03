
using Model.Models;

namespace Service.Interface
{
    public interface IHelperService
    {
        bool CheckBearerTokenIsValidAndNotExpired(string token);
        Task<Customer> GetAdminAccount();
        int GetCustomerIdFromLogged();
        bool IsTokenValid();
    }
}
