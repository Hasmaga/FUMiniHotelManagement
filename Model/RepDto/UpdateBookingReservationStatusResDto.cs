using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class UpdateBookingReservationStatusResDto
    {
        [Required]
        public int BookingReservationId { get; set; }

        [Required]
        public byte BookingStatus { get; set; }
    }
}
