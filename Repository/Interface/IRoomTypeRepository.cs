using Model.Models;

namespace Repository.Interface
{
    public interface IRoomTypeRepository
    {
        Task<bool> CreateRoomTypeAsync(RoomType roomType);
        Task<List<RoomType>> GetAllRoomTypeAsync();
        Task<RoomType?> GetRoomTypeByIdAsync(int roomTypeId);
        Task<bool> UpdateRoomTypeAsync(RoomType roomType);
        Task<bool> DeleteRoomTypeAsync(RoomType roomType);
    }
}
