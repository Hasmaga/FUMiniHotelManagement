using Microsoft.EntityFrameworkCore;
using Model.Models;
using Repository.Interface;

namespace Repository
{
    public class BookingReservationRepository : IBookingReservationRepository
    {
        private readonly FuminiHotelManagementContext _db;

        public BookingReservationRepository(FuminiHotelManagementContext db)
        {
            _db = db;
        }

        public async Task<bool> CreateBookingReservationAsync(BookingReservation booking)
        {
            try
            {
                await _db.BookingReservations.AddAsync(booking);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingReservation>> GetAllBookingReservationAsync()
        {
            try
            {
                return await _db.BookingReservations.ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingReservation>> GetBookingReservationByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _db.BookingReservations.Where(x => x.CustomerId == customerId).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<BookingReservation?> GetBookingReservationByIdAsync(int bookingId)
        {
            try
            {
                return await _db.BookingReservations.FirstOrDefaultAsync(x => x.BookingReservationId == bookingId);
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<BookingReservation>> GetListBookingReservationByCustomerIdAsync(int customerId)
        {
            try
            {
                return await _db.BookingReservations.Where(x => x.CustomerId == customerId).ToListAsync();
            } catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateBookingReservationAsync(BookingReservation booking)
        {
            try
            {
                _db.BookingReservations.Update(booking);
                await _db.SaveChangesAsync();
                return true;
            } catch (Exception)
            {
                throw;
            }
        } 
    }
}
