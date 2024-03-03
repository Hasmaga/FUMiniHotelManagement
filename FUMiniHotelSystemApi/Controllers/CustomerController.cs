using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RepDto;
using Service.Interface;

namespace FUMiniHotelSystemApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;        

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;          
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginResDto loginReqDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var customer = await _customerService.LoginAsync(loginReqDto);                
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateCustomerAsync")]
        public async Task<IActionResult> CreateCustomerAsync([FromBody] CreateCustomerResDto createCustomerResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var customer = await _customerService.CreateCustomerAsync(createCustomerResDto);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllCustomerByAdmin")]
        [Authorize]
        public async Task<IActionResult> GetAllCustomerAsync()
        {
            try
            {
                var customers = await _customerService.GetAllCustomerAsync();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateCustomerByAdmin")]
        [Authorize]
        public async Task<IActionResult> UpdateCustomerAsync([FromBody] UpdateCustomerResDto updateCustomerResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var customer = await _customerService.UpdateCustomerAsync(updateCustomerResDto);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("DeleteCustomerByAdmin")]
        [Authorize]
        public async Task<IActionResult> DeleteCustomerAsync([FromBody] int customerId)
        {
            try
            {
                var customer = await _customerService.DeleteCustomerAsync(customerId);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateProfileByCustomer")]
        [Authorize]
        public async Task<IActionResult> UpdateProfileByCustomerAsync([FromBody] UpdateProfileByCustomerResDto updateProfileByCustomerResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var customer = await _customerService.UpdateProfileByCustomerAsync(updateProfileByCustomerResDto);
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
