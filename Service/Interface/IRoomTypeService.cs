using Model.RepDto;
using Model.ResDto;

namespace Service.Interface
{
    public interface IRoomTypeService
    {
        Task<bool> CreateRoomTypeAsync(CreateRoomTypeResDto roomType);
        Task<List<GetRoomTypeResDto>> GetAllRoomTypeAsync();
        Task<bool> UpdateRoomTypeAsync(UpdateRoomTypeResDto roomType);
    }
}
