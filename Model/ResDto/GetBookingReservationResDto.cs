namespace Model.ResDto
{
    public class GetBookingReservationResDto
    {
        public int BookingReservationId { get; set; }
        public DateOnly BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int CustomerId { get; set; }
        public byte BookingStatus { get; set; }
    }
}
