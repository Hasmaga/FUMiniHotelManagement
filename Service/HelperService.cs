using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Model.Models;
using Repository.Interface;
using Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service
{
    public class HelperService : IHelperService
    {
        private readonly IConfiguration _confi;
        private readonly IHttpContextAccessor _http;
        private readonly ICustomerRepository _customerRepository;

        public HelperService(IConfiguration configuration, IHttpContextAccessor httpContextAccessor, ICustomerRepository customerRepository)
        {
            _confi = configuration;
            _http = httpContextAccessor;
            _customerRepository = customerRepository;
        }

        public bool CheckBearerTokenIsValidAndNotExpired(string token)
        {
            var securityKey = _confi.GetSection("AppSettings:Token").Value ?? throw new Exception("Server Error");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                }, out SecurityToken validatedToken);
                // Check Token Is Expired
                if (validatedToken.ValidTo < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int GetCustomerIdFromLogged()
        {
            var AccId = _http.HttpContext?.User.FindFirst(ClaimTypes.Sid)?.Value;
            return AccId == null ? throw new Exception("Server Error") : int.Parse(AccId);
        }

        public async Task<Customer> GetAdminAccount()
        {
            try
            {
                var emailAdmin = _confi.GetSection("AppSettings:AdminEmail").Value ?? throw new Exception("Server Error");
                return await _customerRepository.GetCustomerByEmailAsync(emailAdmin) ?? throw new Exception("Server Error");
            } catch (Exception)
            {
                throw;
            }
        }

        public bool IsTokenValid()
        {
            var token = _http.HttpContext?.Request.Headers.Authorization.ToString().Replace("Bearer ", "");
            if (token == null || !CheckBearerTokenIsValidAndNotExpired(token))
            {
                return false;
            }
            return true;
        }
    }
}
