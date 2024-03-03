using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class CreateRoomInformationResDto
    {
        [Required]
        public string RoomNumber { get; set; } = null!;
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int RoomTypeId { get; set; }        
        public decimal? RoomPricePerDay { get; set; }
    }
}
