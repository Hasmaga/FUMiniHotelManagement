using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class CreateBookingDetailResDto
    {
        [Required]
        public int BookingReservationId { get; set; }

        public int RoomId { get; set; }

        public DateOnly StartDate { get; set; }

        public DateOnly EndDate { get; set; }
    }
}
