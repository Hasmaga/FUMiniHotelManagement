using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class UpdateRoomInformationResDto
    {
        [Required]
        public int RoomId { get; set; }
        public string? RoomNumber { get; set; }
        public string? RoomDetailDescription { get; set; }
        public int? RoomMaxCapacity { get; set; }
        public int? RoomTypeId { get; set; }        
        public decimal? RoomPricePerDay { get; set; }
    }
}
