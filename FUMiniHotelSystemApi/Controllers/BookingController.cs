using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RepDto;
using Service.Interface;

namespace FUMiniHotelSystemApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("CreateBookingByAdmin")]
        [Authorize]
        public async Task<IActionResult> CreateBookingAsync([FromBody] CreateBookingResDto createBookingResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var booking = await _bookingService.CreateBookingAsync(createBookingResDto);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetListBookingReservationByAdmin")]
        [Authorize]
        public async Task<IActionResult> GetListBookingReservationAsync()
        {
            try
            {
                var booking = await _bookingService.GetListBookingReservationAsync();
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateBookingReservationStatusByAdmin")]
        [Authorize]
        public async Task<IActionResult> UpdateBookingReservationStatusAsync([FromBody] UpdateBookingReservationStatusResDto updateBookingReservationStatusResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var booking = await _bookingService.UpdateBookingReservationStatusAsync(updateBookingReservationStatusResDto);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllBookingDetailByReservationIdByAdmin")]
        [Authorize]
        public async Task<IActionResult> GetAllBookingDetailByReservationIdAsync(int reservationId)
        {
            try
            {
                var booking = await _bookingService.GetAllBookingDetailByReservationIdAsync(reservationId);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("DeleteBookingDetailByAdmin")]
        [Authorize]
        public async Task<IActionResult> DeleteBookingDetailAsync([FromBody] DeleteBookingDetailResDto deleteBookingDetailResDto)
        {
            try
            {
                var booking = await _bookingService.DeleteBookingDetailAsync(deleteBookingDetailResDto);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateBookingDetailByAdmin")]
        [Authorize]
        public async Task<IActionResult> CreateBookingDetailAsync([FromBody] CreateBookingDetailResDto createBookingDetailResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var booking = await _bookingService.CreateBookingDetailAsync(createBookingDetailResDto);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateBookingDetailByAdmin")]
        [Authorize]
        public async Task<IActionResult> UpdateBookingDetailAsync([FromBody] UpdateBookingDetailResDto updateBookingDetailResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var booking = await _bookingService.UpdateBoookingDetailAsync(updateBookingDetailResDto);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateBookingByCustomer")]
        [Authorize]
        public async Task<IActionResult> CreateBookingByCustomerAsync([FromBody] CreateBookingForCustomerResDto createBookingForCustomerResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var booking = await _bookingService.CreateBookingByCustomerAsync(createBookingForCustomerResDto);
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetListBookingReservationByCustomer")]
        [Authorize]
        public async Task<IActionResult> GetListBookingReservationByCustomerAsync()
        {
            try
            {
                var booking = await _bookingService.GetListBookingReservationByCustomerAsync();
                return Ok(booking);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
