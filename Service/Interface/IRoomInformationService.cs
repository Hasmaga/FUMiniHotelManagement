using Model.RepDto;
using Model.ResDto;

namespace Service.Interface
{
    public interface IRoomInformationService
    {
        Task<bool> CreateRoomInformationAsync(CreateRoomInformationResDto room);
        Task<List<GetRoomInformationResDto>> GetAllRoomInformationAsync();
        Task<bool> UpdateRoomInformationAsync(UpdateRoomInformationResDto room);
        Task<bool> DeleteRoomInformationAsync(int roomId);
    }
}
