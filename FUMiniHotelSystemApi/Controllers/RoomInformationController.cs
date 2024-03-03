using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.RepDto;
using Service.Interface;

namespace FUMiniHotelSystemApi.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class RoomInformationController : Controller
    {
        private readonly IRoomInformationService _roomInformationService;

        public RoomInformationController(IRoomInformationService roomInformationService)
        {
            _roomInformationService = roomInformationService;
        }

        [HttpPost("CreateRoomInformationAsync")]
        [Authorize]
        public async Task<IActionResult> CreateRoomInformationAsync([FromBody] CreateRoomInformationResDto createRoomInformationResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var room = await _roomInformationService.CreateRoomInformationAsync(createRoomInformationResDto);
                return Ok(room);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetAllRoomInformationAsync")]
        public async Task<IActionResult> GetAllRoomInformationAsync()
        {
            try
            {
                var room = await _roomInformationService.GetAllRoomInformationAsync();
                return Ok(room);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("UpdateRoomInformationAsync")]
        [Authorize]
        public async Task<IActionResult> UpdateRoomInformationAsync([FromBody] UpdateRoomInformationResDto updateRoomInformationResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Invalid request");
                }
                var room = await _roomInformationService.UpdateRoomInformationAsync(updateRoomInformationResDto);
                return Ok(room);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("DeleteRoomInformationAsync")]
        [Authorize]
        public async Task<IActionResult> DeleteRoomInformationAsync([FromBody] int roomId)
        {
            try
            {
                var room = await _roomInformationService.DeleteRoomInformationAsync(roomId);
                return Ok(room);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
