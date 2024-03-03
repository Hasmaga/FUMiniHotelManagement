using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Interface;

namespace Repository
{
    public class RoomInformationRepository : IRoomInformationRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public RoomInformationRepository(FuminiHotelManagementContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateRoomInformationAsync(RoomInformation room)
        {
            try
            {
                await _db.RoomInformations.AddAsync(room);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRoomInformationAsync(RoomInformation room)
        {
            try
            {
                _db.RoomInformations.Remove(room);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<RoomInformation>> GetAllRoomInformationAsync()
        {
            try
            {
                return await _db.RoomInformations.ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomInformation?> GetRoomInformationById(int roomId)
        {
            try
            {
                return await _db.RoomInformations.FindAsync(roomId);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomInformation?> GetRoomInformationByIdAsync(int roomId)
        {
            try
            {
                return await _db.RoomInformations.FindAsync(roomId);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<RoomInformation?> GetRoomInformationByRoomNumberAsync(string RoomNumber)
        {
            try
            {
                return await _db.RoomInformations.FirstOrDefaultAsync(x => x.RoomNumber == RoomNumber);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateRoomInformationAsync(RoomInformation room)
        {
            try
            {
                _db.RoomInformations.Update(room);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
