using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class UpdateBookingDetailResDto
    {
        [Required]
        public int BookingReservationId { get; set; }

        [Required]
        public int RoomId { get; set; }

        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set;}
    }
}
