using Model.Models;

namespace Repository.Interface
{
    public interface IBookingDetailRepository
    {
        Task<bool> CreateBookingDetailAsync(BookingDetail booking);
        Task<bool> IsRoomAvailableAsync(int roomId, DateOnly checkIn, DateOnly checkOut);
        Task<List<BookingDetail>> GetAllBookingDetailByReservationIdAsync(int reservationId);
        Task<BookingDetail?> GetBookingDetailByBoookingReservationAndRoomIdAsync(int reservationId, int roomId);
        Task<bool> UpdateBookingDetailAsync(BookingDetail booking);
        Task<bool> DeleteBookingDetailAsync(BookingDetail booking);
    }
}
