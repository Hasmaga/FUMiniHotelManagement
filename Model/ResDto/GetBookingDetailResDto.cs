namespace Model.ResDto
{
    public class GetBookingDetailResDto
    {        
        public string? RoomNumber { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public decimal ActualPrice { get; set; }
    }
}
