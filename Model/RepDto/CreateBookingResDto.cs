using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class CreateBookingResDto
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<int> RoomIds { get; set; } = null!; 

        [Required]
        public DateOnly StartDate { get; set; }

        [Required]
        public DateOnly EndDate { get; set; }
    }
}
