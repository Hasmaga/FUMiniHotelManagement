using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class DeleteBookingDetailResDto
    {
        [Required]
        public int BookingReservationId { get; set; }

        [Required]
        public int RoomId { get; set; }
    }
}
