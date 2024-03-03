using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RepDto;
using Service.Interface;

namespace FUMiniHotelSystemApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomTypeController : Controller
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeController(IRoomTypeService roomTypeService)
        {
            _roomTypeService = roomTypeService;
        }

        [HttpPost("CreateRoomTypeByAdmin")]
        [Authorize]
        public async Task<IActionResult> CreateRoomTypeAsync([FromBody] CreateRoomTypeResDto createRoomTypeResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var roomType = await _roomTypeService.CreateRoomTypeAsync(createRoomTypeResDto);
                return Ok(roomType);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllRoomTypeByAdmin")]
        [Authorize]
        public async Task<IActionResult> GetAllRoomTypeAsync()
        {
            try
            {
                var roomType = await _roomTypeService.GetAllRoomTypeAsync();
                return Ok(roomType);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateRoomTypeByAdmin")]
        [Authorize]
        public async Task<IActionResult> UpdateRoomTypeAsync([FromBody] UpdateRoomTypeResDto updateRoomTypeResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var roomType = await _roomTypeService.UpdateRoomTypeAsync(updateRoomTypeResDto);
                return Ok(roomType);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
