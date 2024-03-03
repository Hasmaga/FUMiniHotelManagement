using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Interface;

namespace Repository
{
    public class RoomTypeRepository : IRoomTypeRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public RoomTypeRepository(FuminiHotelManagementContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateRoomTypeAsync(RoomType roomType)
        {
            try
            {
                await _db.RoomTypes.AddAsync(roomType);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRoomTypeAsync(RoomType roomType)
        {
            try
            {
                _db.RoomTypes.Remove(roomType);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RoomType>> GetAllRoomTypeAsync()
        {
            try
            {
                return await _db.RoomTypes.ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomType?> GetRoomTypeByIdAsync(int roomTypeId)
        {
            try
            {
                return await _db.RoomTypes.FindAsync(roomTypeId);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRoomTypeAsync(RoomType roomType)
        {
            try
            {
                _db.RoomTypes.Update(roomType);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
