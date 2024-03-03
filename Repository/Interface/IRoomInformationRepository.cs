using Model.Models;

namespace Repository.Interface
{
    public interface IRoomInformationRepository
    {
        Task<bool> CreateRoomInformationAsync(RoomInformation room);
        Task<List<RoomInformation>> GetAllRoomInformationAsync();
        Task<RoomInformation?> GetRoomInformationByIdAsync(int roomId);
        Task<bool> UpdateRoomInformationAsync(RoomInformation room);
        Task<bool> DeleteRoomInformationAsync(RoomInformation room);
        Task<RoomInformation?> GetRoomInformationByRoomNumberAsync(string RoomNumber);
        Task<RoomInformation?> GetRoomInformationById(int roomId);
    }
}
