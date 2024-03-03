using Model.Models;
using Model.RepDto;
using Model.ResDto;

namespace Service.Interface
{
    public interface IBookingService
    {
        Task<bool> CreateBookingAsync(CreateBookingResDto booking);
        Task<List<GetBookingReservationResDto>> GetListBookingReservationAsync();
        Task<bool> UpdateBookingReservationStatusAsync(UpdateBookingReservationStatusResDto update);
        Task<List<GetBookingDetailResDto>> GetAllBookingDetailByReservationIdAsync(int reservationId);
        Task<bool> DeleteBookingDetailAsync(DeleteBookingDetailResDto delete);
        Task<bool> CreateBookingDetailAsync(CreateBookingDetailResDto create);
        Task<bool> UpdateBoookingDetailAsync(UpdateBookingDetailResDto update);
        Task<bool> CreateBookingByCustomerAsync(CreateBookingForCustomerResDto booking);
        Task<List<GetBookingReservationResDto>> GetListBookingReservationByCustomerAsync();
    }
}
