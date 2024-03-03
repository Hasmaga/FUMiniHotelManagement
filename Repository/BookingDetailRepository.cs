using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Interface;

namespace Repository
{
    public class BookingDetailRepository : IBookingDetailRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public BookingDetailRepository(FuminiHotelManagementContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateBookingDetailAsync(BookingDetail booking)
        {
            try
            {
                await _db.BookingDetails.AddAsync(booking);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteBookingDetailAsync(BookingDetail booking)
        {
            try
            {
                _db.BookingDetails.Remove(booking);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingDetail>> GetAllBookingDetailByReservationIdAsync(int reservationId)
        {
            try
            {
                return await _db.BookingDetails.Where(b => b.BookingReservationId == reservationId).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingDetail?> GetBookingDetailByBoookingReservationAndRoomIdAsync(int reservationId, int roomId)
        {
            try
            {
                return await _db.BookingDetails
                    .Where(b => b.BookingReservationId == reservationId)
                    .Where(b => b.RoomId == roomId)
                    .FirstOrDefaultAsync();
            } catch (Exception)
            {
                throw;
            }
        }        

        public async Task<bool> IsRoomAvailableAsync(int roomId, DateOnly checkIn, DateOnly checkOut)
        {
            try
            {                                
                var booking = await _db.BookingDetails
                    .Where(b => b.RoomId == roomId)
                    .Where(b => b.StartDate < checkOut && b.EndDate > checkIn)
                    .FirstOrDefaultAsync();
                if (booking == null)
                {              
                    return true;
                }
                var bookingReservation = await _db.BookingReservations
                        .Where(b => b.BookingReservationId == booking.BookingReservationId)
                        .Where(b => b.BookingStatus != 0)
                        .FirstOrDefaultAsync();
                if (bookingReservation == null)
                {
                    return true;
                }
                return false;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBookingDetailAsync(BookingDetail booking)
        {
            try
            {
                _db.BookingDetails.Update(booking);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }
    }
}
