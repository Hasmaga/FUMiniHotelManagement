using Model.Models;

namespace Repository.Interface
{
    public interface IBookingReservationRepository
    {
        Task<bool> CreateBookingReservationAsync(BookingReservation booking);
        Task<bool> UpdateBookingReservationAsync(BookingReservation booking);
        Task<BookingReservation?> GetBookingReservationByIdAsync(int bookingId);
        Task<List<BookingReservation>> GetAllBookingReservationAsync();        
        Task<List<BookingReservation>> GetBookingReservationByCustomerIdAsync(int customerId);
        Task<List<BookingReservation>> GetListBookingReservationByCustomerIdAsync(int customerId);

    }
}
