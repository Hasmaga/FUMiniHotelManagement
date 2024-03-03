using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Model.RepDto;
using Model.ResDto;
using Repository.Interface;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IConfiguration _configuration;
        private readonly IHelperService _helperService;

        public CustomerService(ICustomerRepository customerRepository, IConfiguration configuration, IHelperService helperService)
        {
            _customerRepository = customerRepository;
            _configuration = configuration;
            _helperService = helperService;
        }

        public async Task<bool> CreateCustomerAsync(CreateCustomerResDto createCustomerResDto)  // By Admin vs Customer
        {
            try 
            {
                var isExist = await _customerRepository.GetCustomerByEmailAsync(createCustomerResDto.EmailAddress);
                if (isExist != null)
                {
                    throw new Exception("Email already exists");
                }
                var customer = new Customer()
                {
                    CustomerFullName = createCustomerResDto.CustomerFullName,
                    EmailAddress = createCustomerResDto.EmailAddress,
                    Telephone = createCustomerResDto.Telephone,
                    CustomerBirthday = createCustomerResDto.CustomerBirthday,
                    Password = createCustomerResDto.Password
                };
                return await _customerRepository.CreateCustomerAsync(customer);
            } catch (Exception)
            {
                throw;
            }            
        }

        public async Task<bool> DeleteCustomerAsync(int customerId) // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var customer = await _customerRepository.GetCustomerByIdAsync(customerId) ?? throw new Exception("Customer not found");
                // status customer is byte type, 1 is active, 0 is inactive
                customer.CustomerStatus = 0;
                return await _customerRepository.UpdateCustomerAsync(customer);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetCustomerResDto>> GetAllCustomerAsync()  // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var customers = await _customerRepository.GetAllCustomerAsync();
                return customers.Select(x => new GetCustomerResDto()
                {
                    CustomerId = x.CustomerId,
                    CustomerFullName = x.CustomerFullName,
                    EmailAddress = x.EmailAddress,
                    Telephone = x.Telephone,
                    CustomerBirthday = x.CustomerBirthday,
                    CustomerStatus = x.CustomerStatus
                }).ToList();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> LoginAsync(LoginResDto loginResDto)
        {
            try
            {
                var customer = await _customerRepository.GetCustomerByEmailAsync(loginResDto.Email) ?? throw new Exception("Customer not found");
                if (customer.Password != loginResDto.Password)
                {
                    throw new Exception("Invalid password");
                }
                if (customer.CustomerStatus == 0)
                {
                    throw new Exception("Customer is inactive");
                }
                return CreateBearerTokenAccount(customer);
            } catch (Exception)
            {
                throw;
            }
                       
        }

        public async Task<bool> UpdateCustomerAsync(UpdateCustomerResDto updateCustomerResDto) // By Admin
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                var adminAccount = await _helperService.GetAdminAccount();
                if (AccountLoggedIn.CustomerId != adminAccount.CustomerId)
                {
                    throw new Exception("Unauthorized");
                }
                var customer = await _customerRepository.GetCustomerByIdAsync(updateCustomerResDto.CustomerId) ?? throw new Exception("Customer not found");
                if (updateCustomerResDto.CustomerFullName != null)
                {
                    customer.CustomerFullName = updateCustomerResDto.CustomerFullName;
                }

                if (updateCustomerResDto.Telephone != null)
                {
                    customer.Telephone = updateCustomerResDto.Telephone;
                }

                if (updateCustomerResDto.CustomerBirthday.HasValue)
                {
                    customer.CustomerBirthday = updateCustomerResDto.CustomerBirthday.Value;
                }

                if (updateCustomerResDto.Password != null)
                {
                    customer.Password = updateCustomerResDto.Password;
                }

                return await _customerRepository.UpdateCustomerAsync(customer);
            }
            catch (Exception)
            {
                throw;
            }            
        }

        public async Task<bool> UpdateProfileByCustomerAsync(UpdateProfileByCustomerResDto updateProfileByCustomerResDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception("Unauthorized");
                }
                var AccountLoggedIn = await _customerRepository.GetCustomerByIdAsync(_helperService.GetCustomerIdFromLogged()) ?? throw new Exception("Unauthorized");
                AccountLoggedIn.CustomerFullName = updateProfileByCustomerResDto.CusomerFullName ?? AccountLoggedIn.CustomerFullName;
                AccountLoggedIn.Telephone = updateProfileByCustomerResDto.Telephone ?? AccountLoggedIn.Telephone;
                AccountLoggedIn.CustomerBirthday = updateProfileByCustomerResDto.CustomerBirthday ?? AccountLoggedIn.CustomerBirthday;
                AccountLoggedIn.Password = updateProfileByCustomerResDto.Password ?? AccountLoggedIn.Password;
                return await _customerRepository.UpdateCustomerAsync(AccountLoggedIn);
            } catch (Exception)
            {
                throw;
            }
        }


        #region Private Methods

        private string CreateBearerTokenAccount(Customer loginedAcc)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Sid, loginedAcc.CustomerId.ToString()),
            ];
            var securityKey = _configuration.GetSection("AppSettings:Token").Value ?? throw new Exception("SERVER_ERROR");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token == null ? throw new Exception("SERVER_ERROR") : tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
