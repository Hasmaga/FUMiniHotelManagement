using System.ComponentModel.DataAnnotations;

namespace Model.RepDto
{
    public class CreateRoomTypeResDto
    {
        [Required]
        public string RoomTypeName { get; set; } = null!;
        public string? TypeDescription { get; set; } 
        public string? TypeNote { get; set; }
    }
}
